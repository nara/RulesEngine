using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Rhino.DSL;

namespace RulesEngine.BooEvaluator
{
    public class RuleDslEngine<M> : DslEngine where M : DslModel
    {
        public override CompilerContext ForceCompile(string[] urls, string cacheFileName)
        {
            var booCompiler = new BooCompiler();
            booCompiler.Parameters.OutputType = CompilerOutputType;
            booCompiler.Parameters.GenerateInMemory = true;
            booCompiler.Parameters.Pipeline = new CompileToMemory();
            CustomizeCompiler(booCompiler, booCompiler.Parameters.Pipeline, new string[]{});
            AddInputs(urls, booCompiler);
            var compilerContext = booCompiler.Run();
            CompileCompleted(compilerContext);
            return compilerContext;
        }

        protected override void CustomizeCompiler(BooCompiler compiler, CompilerPipeline pipeline, string[] urls)
        {
            compiler.Parameters.AddAssembly(typeof(BooCompiler).Assembly);
            compiler.Parameters.Ducky = true;
            pipeline.Insert(1, new ImplicitBaseClassCompilerStep(typeof (M), "Prepare"));
            base.CustomizeCompiler(compiler, pipeline, urls);
        }

        private void AddInputs(string[] urls, BooCompiler compiler)
        {
            foreach (var url in urls)
            {
                compiler.Parameters.Input.Add(Storage.CreateInput(url));
            }
        }

        private void CompileCompleted(CompilerContext context)
        {
            if(context.Errors.Count != 0)
            {
                throw CreateCompilerException(context);
            }
            HandleWarnings(context.Warnings);
        }
        
    }
}