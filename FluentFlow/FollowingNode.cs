namespace FluentFlow
{
    public class FollowingNode<T> : FlowElement<T>
    {
        public FlowNode<T> Then
        {
            get
            {
                return new FlowNode<T>();
            }
        }

        public override void Evaluate(T instance)
        {
            throw new System.NotImplementedException();
        }
    }
}