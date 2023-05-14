using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    [SerializeField]
    private DialogueSettings _settings;
    private List<string> _npcDialogues;

    private bool playerHit;
    // Start is called before the first frame update
    void Start()
    {
        _npcDialogues = GetDialogues().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
            DialogueControl.instance.Speech(_npcDialogues);
    }

    private void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        if (hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

    private IEnumerable<string> GetDialogues()
    {
        switch (DialogueControl.instance.CurrentIdiom)
        {
            case DialogueControl.Idiom.eng:
                return _settings.dialogues.Select(x => x.sentence.english);
            case DialogueControl.Idiom.spa:
                return _settings.dialogues.Select(x => x.sentence.spanish);
            case DialogueControl.Idiom.pt:
            default:
                return _settings.dialogues.Select(x => x.sentence.portuguese);
        }
    }
}
