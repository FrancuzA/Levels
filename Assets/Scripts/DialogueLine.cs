using System;

[Serializable]
public class DialogueLine
{
    public string text; // Tekst mówiony przez postaæ
    public DialogueOption[] options; // Opcje odpowiedzi gracza
}