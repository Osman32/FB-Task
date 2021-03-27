using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private NavMeshAgent agent;
    private ThirdPersonCharacter character;
    private bool isDead;
    private bool isWin;
    private float x = 0;

    #region Private Methods
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        character.m_MoveSpeedMultiplier = speed / 10f;
    }

    private void Update()
    {
        x = Mathf.MoveTowards(x, SimpleInput.GetAxis("Horizontal"), Time.deltaTime * 10f);

        Vector3 target = new Vector3(x, 0, 1);
        character.Move(target, false, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(Win());
        }
    }

   
    #endregion

    #region Public Methods
    public IEnumerator Dead()
    {
        if (!isDead)
        {
            isDead = true;
            GameObject.Find("TextDisplay").GetComponent<Text>().enabled = true;
            GameObject.Find("TextDisplay").GetComponent<Text>().text = "You Lose!";
            yield return new WaitForSeconds(0.1f);
            Time.timeScale = 0f;
        }
    }
    public IEnumerator Win()
    {
        if (!isWin)
        {
            isWin = true;
            GameObject.Find("TextDisplay").GetComponent<Text>().text = "You Win!";
            FindObjectOfType<CameraFollow>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Animator>().SetBool("Win", true);
            GetComponent<Animator>().SetLayerWeight(2, 0f);

            GetComponent<IKHandling>().leftIKWeight = 0;
            GetComponent<IKHandling>().rightIKWeight = 0;

            character.Move(Vector3.zero, false, false);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    #endregion
}
