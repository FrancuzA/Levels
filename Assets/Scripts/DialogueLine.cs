using System;

[Serializable]
public class DialogueLine
{
    public string text; // Tekst m�wiony przez posta�
    public DialogueOption[] options; // Opcje odpowiedzi gracza
}