namespace Entitas
{
    /// Automatic EntityExt Reference Counting (AERC)
    /// is used internally to prevent pooling retained entities.
    /// If you use retain manually you also have to
    /// release it manually at some point.
    /// UnsafeAERC doesn't check if the entity has already been
    /// retained or released. It's faster, but you lose the information
    /// about the owners.
    public sealed class UnsafeAERC : IAERC
    {
        int _retainCount;

        public int RetainCount
        {
            get { return _retainCount; }
        }

        public void Retain(object owner, bool throwIfRepeated = true)
        {
            _retainCount += 1;

        }

        public void Release(object owner, bool throwIfNotExisted = true)
        {
            _retainCount -= 1;
        }

    }
}