using Sharpen;
using com.wd.free.para;
using com.wd.free.unit;
using gameplay.gamerule.free.action;
using Core.Free;
using com.wd.free.trigger;
using gameplay.gamerule.free.component;
using App.Shared.FreeFramework.framework.trigger;
using com.wd.free.map;
using App.Shared.FreeFramework.framework.@event;
using com.wd.free.action;

namespace com.wd.free.@event
{
    public interface IEventArgs : IFreeArgs
    {
        void AddDefault(IParable para);

        IParable GetUnit(string key);

        void SetPara(string key, IParable paras);

        // ��ʱʹ�ö���
        void TempUse(string key, IParable paras);

        // �ͷ���ʱʹ�ö���
        void Resume(string key);

        // ��ʱʹ��ȫ�ֱ���
        void TempUsePara(IPara para);

        // �ͷ�ȫ����ʱʹ�ñ���
        void ResumePara(string paraName);

        IParable RemovePara(string key);

        IParable GetDefault();

        Contexts GameContext { get; }

        FreeContext FreeContext { get; }

        string[] GetUnitKeys();

        CommonActions Functions
        {
            get;
        }

        GameTriggers Triggers
        {
            get;
        }

        GameComponentMap ComponentMap
        {
            get; set;
        }

        GameUnitSet GetGameUnits();

        void Trigger(int eventId);

        void Trigger(int eventId, TriggerArgs args);

        void Trigger(int eventId, IPara para);

        void Trigger(int eventId, params TempUnit[] units);

        void Trigger(int eventId, TempUnit[] units, IPara[] paras);

        void Act(IGameAction action);

        void Act(IGameAction action, TriggerArgs args);

        void Act(IGameAction action, params TempUnit[] units);

        int GetInt(string v);

        float GetFloat(string v);

        bool GetBool(string v);

        double getDouble(string v);

        long GetLong(string v);

        string GetString(string v);
    }

    public static class IEventArgsConstants
    {

    }
}
