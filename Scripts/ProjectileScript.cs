using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] bool selected = false;
    [SerializeField] Rigidbody2D projectileRigid;
    public GameObject explosion;
    public Vector2 hitPoint;
    public Text hitToFixatorTxt;
    public float hitToFixator;
    public float projectileSpeed;
    public Text projectileSpeedTxt;

    // V = (MS / mx) * (sqrt(9.8 * L) / 6)
    // m - масса птички
    // М - масса маятника
    // S - отклонение маятника от положения равновесия
    // L - длина маятника
    // х - расстояние от точки удара до верхней точки маятника (hitTiFixator)

    int L = 6;
    public float mb;
    public float M;
    public float S;



    public void SliderChanged(float newMass)
    {
        GetComponent<Rigidbody2D>().mass = newMass;
        mb = newMass;
    }

    private void OnMouseDown()
    {
        selected = true;
        projectileRigid.isKinematic = true;
    }

    private void OnMouseUp()
    {
        selected = false;
        projectileRigid.isKinematic = false; 
        StartCoroutine(LetItGo());
    }

    private void Start()
    {
        projectileRigid = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (selected == true)
        {
            projectileRigid.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

        }
    }

    IEnumerator LetItGo() //, let it go, can't hold it back anymore
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // If a missile hits this object
        if (coll.transform.tag == "Target")
        {
            // Spawn an explosion at each point of contact
            foreach (ContactPoint2D missileHit in coll.contacts)
            {
                hitPoint = missileHit.point;
                hitToFixator = 3.72f - hitPoint.y; // 3.72 - координата верхней точки маятника, да да, волшебные числа, мне лень делать классы под каждый объект на экране))
                hitToFixatorTxt.text = hitToFixator.ToString();
                Instantiate(explosion, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
                //Destroy(gameObject);
                StartCoroutine(CalculateSpeed());
            }
        }
    }

    IEnumerator CalculateSpeed()
    {
        yield return new WaitForSeconds(2f);
        projectileSpeed = (((M * 0.001f) * (S * 0.01f)) / ((mb * 0.001f) * (hitToFixator * 0.01f))) * Mathf.Sqrt((9.8f * (L * 0.01f)) / 6);
        projectileSpeedTxt.text = projectileSpeed.ToString();
    }
}
