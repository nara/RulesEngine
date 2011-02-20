namespace FluentFlow
{
    public abstract class FlowElement<T>
    {
        protected FlowElement<T> master;

        protected FlowElement()
        {
            master = this;
        }

        protected FlowElement(FlowElement<T> master)
        {
            this.master = master;    
        }
        internal abstract void Evaluate(T instance);

        public virtual void Execute(T instance)
        {
            if(this.master != null) 
                this.master.Evaluate(instance);
            else
                this.Evaluate(instance);
        }
    }
}