using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.Data
{
    public interface ISaveLoadData
    {
        public UniTask Load();
        public void Save();
    }
}