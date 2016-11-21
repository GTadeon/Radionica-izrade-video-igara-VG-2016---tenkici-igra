using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathDefinition : MonoBehaviour {

	public Transform[] tocke;

    public enum TankDirection { up, down, left, right}
    public TankDirection[] tankOnThisPoint;

    public FollowPath followPath;


	public IEnumerator<Transform> DohvatiPutanju()
	{
		if (tocke.Length<1 || tocke==null)
		{
			yield break;
		}

		int smjer = 1;
		int indeksTrenutnePlatforme = 0;

		while (true) 
		{
			yield return tocke [indeksTrenutnePlatforme];
			if (tocke.Length==1)
				continue;
			if (indeksTrenutnePlatforme<=0)
				smjer=1;
			else if (indeksTrenutnePlatforme>=tocke.Length-1)
				smjer=-1;
			indeksTrenutnePlatforme=indeksTrenutnePlatforme+smjer;

            if (smjer == 1)
            {
                followPath.RotateTankApropriately(indeksTrenutnePlatforme);
            }
            else
            {
                followPath.RotateTankBySide(GetOpositeSideOf(tankOnThisPoint[indeksTrenutnePlatforme+1]));
                Debug.Log("tank je trebao ici u ovom smjeru kad idem unazad  " + tankOnThisPoint[indeksTrenutnePlatforme+1]+ " al je oso na ovu: "+ GetOpositeSideOf(tankOnThisPoint[indeksTrenutnePlatforme+1]));
            }

        }
    }


    public TankDirection GetOpositeSideOf(TankDirection side)
    {
        if (side==TankDirection.up)
        {
            return TankDirection.down;
        }
        else if (side == TankDirection.down)
        {
            return TankDirection.up;
        }
        else if (side == TankDirection.right)
        {
            return TankDirection.left;
        }
        else
        {
            return TankDirection.right;
        }
    }

    public void OnDrawGizmos()
	{
		if (tocke==null || tocke.Length<2)
		{
			return;
		}
		for (int i=1; i<tocke.Length; i++)
		{
			Gizmos.DrawLine(tocke[i-1].position,tocke[i].position);
		}
	}
}
