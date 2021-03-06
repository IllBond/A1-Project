using System.Collections;
using UnityEngine;

public class RobberyState : State
{
    private float startVal;
    public RobberyState(RobberController character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {

        startVal = character.roberryPathFinder.movePositionHouse.Property;
        character.roberryPathFinder.movePositionHouse.rob = true;
        character.StartCoroutineTMP(Wringer());
        character.roberryPathFinder.movePositionHouse.OnCompleteRobbir += EndRobbie;

        character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetBGText(true);
    }


    IEnumerator Wringer() {
        float val = 0;
        float time = 0;
        while (character.roberryPathFinder.movePositionHouse.Property > 0)
        {


            //if (!character.roberryPathFinder.movePositionHouse.securityProtected) { 
 
                /*            float val = Mathf.Round(character.roberryPathFinder.movePositionHouse.upg_zabor_or_signalization ?
                                character.factorPropertyperSecodn / 1.5f :
                                character.factorPropertyperSecodn);     */


                    
            val = Mathf.Round(character.roberryPathFinder.movePositionHouse.upg_zabor_or_signalization ?
                character.factorPropertyperSecodn / (Metric.Instance.isOnMetric ? Metric.Instance.signalizationValue.GetComponent<MetricaVal>().value : 1.3f) :
                character.factorPropertyperSecodn);

            time = Mathf.Round(character.roberryPathFinder.movePositionHouse.Property / val);

            character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetNewTime(""+ time, true);
            character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetText(""+  Mathf.Round(100 - (100 * character.roberryPathFinder.movePositionHouse.Property) / startVal) + " %", false);
            yield return new WaitForSeconds(1);

            if (character.roberryPathFinder.movePositionHouse.moveProtected)
            {
                if (character.roberryPathFinder.movePositionHouse.Property - val <= 0)
                {
                    character.StopCoroutineTMP();
                }

                character.roberryPathFinder.movePositionHouse.Property = val;
            }
            else {

                character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetBGText(false);
                character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetBG(false);
            }

            
        }
    }


    public void EndRobbie()
    {
        stateMachine.ChangeState(character.escaping);
    }

    public override void HandleInput()
    { 
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    { }

    public override void Exit()
    {
        character.roberryPathFinder.movePositionHouse.OnCompleteRobbir -= EndRobbie;
        character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetNewTime("", false);
        character.roberryPathFinder.movePositionHouse.marker.GetComponent<UITimer>().SetBGText(false);


    }
}
