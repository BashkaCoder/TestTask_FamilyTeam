using UnityEngine;

namespace _2D.Scripts.Capabilities
{
    //интерфейс для взаимодействиями с компонентами, отвечающими за передвижение
    public interface IMove
    {
        void SetVelocity(Vector2 direction, float speed);
    }
}