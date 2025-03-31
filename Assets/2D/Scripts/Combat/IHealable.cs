namespace _2D.Scripts.Combat
{
    //интерфейс для взаимодействиями с объектами, которые можно полечить
    public interface IHealable
    {
        void Heal(int amount);
    }
}