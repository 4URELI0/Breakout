using UnityEngine;

public interface IPowerUpEffect
{
    void Apply(Paddle paddle);
    void Remove(Paddle paddle); // Para deshacer el efecto si es necesario.
}


