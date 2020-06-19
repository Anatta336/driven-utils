namespace SamDriver.Util
{
    public interface ISuppressable
    {
        bool IsSuppressed { get; }
        void AddSuppress(object suppressor);
        void RemoveSuppress(object suppressor);

        // -- Typical implementation --
        // public bool IsSuppressed { get => (bool)suppressionStack; }
        // StackingBool suppressionStack = new StackingBool();
        // public void AddSuppress(object suppressor) => suppressionStack.AddTrue(suppressor);
        // public void RemoveSuppress(object suppressor) => suppressionStack.RemoveTrue(suppressor);
    }
}
