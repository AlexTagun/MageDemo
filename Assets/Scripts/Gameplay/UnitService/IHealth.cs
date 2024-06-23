public interface IHealth
{
    float GetCurrent();
    float GetMax();
    void ReceiveHit(float damage);
    void ReceiveHeal(float heal);
}