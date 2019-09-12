namespace Entitas {

    public interface IAERC {
        
        int RetainCount { get; }
        void Retain(object owner,bool throwIfRepeated=true);
        void InternalRelease(object owner,bool throwIfNotExisted=true);
    }
}
