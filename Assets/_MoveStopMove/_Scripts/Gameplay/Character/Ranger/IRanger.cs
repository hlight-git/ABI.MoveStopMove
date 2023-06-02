
public interface IRanger : ICharacter, IBoostable<AbOnRangerBooster>, IBoostable
{
    int Score { get; }
    float AttackSpeed { get; }
    float AttackRange { get; }
    Weapon Weapon { get; }
    void AddScore(int score);
    void SetMoveSpeed(float moveSpeed);
    void SetAttackSpeed(float attackSpeed);
    void SetAttackRange(float attackRange);
    void AddEnemy(ICharacter target);
    void RemoveEnemy(ICharacter target);
    ICharacter GetNearestEnemy();
    ICharacter GetOldestEnemy();
    bool IsEnemyInRange(ICharacter enemy);
    void OnAnEnemyGetInRange(ICharacter enemy);
    void OnAnEnemyOutOfRange(ICharacter enemy);
}
