using UnityEngine;

public class Shooter : MonoBehaviour
{
    //Add objectPooler

    public void Shoot(Bullet prefab)
    {
        //Refactor with pooler
        var bullet = Instantiate(prefab, transform.position, transform.rotation);
        bullet.SetPooler();
    }
}
