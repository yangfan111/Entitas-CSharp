namespace Entitas {

    public interface IAERC {
        
        int retainCount { get; }
        void Retain(object owner,bool throwIfRepeated=true);
        void Release(object owner,bool throwIfNotExisted=true);
    }
}
