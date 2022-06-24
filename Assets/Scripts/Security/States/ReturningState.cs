using UnityEngine;

public class ReturningState : StateSecurity
{
    FactoriesSecurity factoriesSecurity = TargetsManager.Instance.factoriesSecurity;
    bool stoped;

    public ReturningState(SecurityController character, StateMachineSecurity stateMachine) : base(character, stateMachine)
    {

    }

    public override void Enter()
    {
        character.tartgetHouse._house.securityProtected = false;

        character.pathMover.StartMove();
        character.pathMover._navMeshAgent.destination = factoriesSecurity._spawnPoint.transform.position;
    }

    public override void HandleInput()
    { }

    public override void LogicUpdate()
    { }

    public override void PhysicsUpdate()
    {
        if (Vector3.Distance(factoriesSecurity._spawnPoint.transform.position, character.transform.position) < 1f && !stoped)
        {
            stoped = true;
            character.pathMover.StopMove();
            character.Escape();
        }

    }

    public override void Exit()
    {

    }
}
