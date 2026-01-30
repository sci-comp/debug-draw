namespace World
{
    public interface IDebugDrawable
    {
#if TOOLS
        void DrawDebug();
#endif
    }

}

