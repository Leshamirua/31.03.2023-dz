using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _31._03._2023_dz
{
    public class Memento
    {
        public string Text { get; private set; }

        public Memento(string text)
        {
            Text = text;
        }
    }
    public class TextEditor
    {
        private string text;
        private Stack<Memento> undoStack = new Stack<Memento>(256);
        private Stack<Memento> redoStack = new Stack<Memento>(256);

        public TextEditor()
        {
            text = "";
        }

        public void SetText(string text)
        {
            SaveStateToUndoStack();
            text = text;
            ClearRedoStack();
        }

        public string GetText()
        {
            return text;
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                SaveStateToRedoStack();
                Memento memento = undoStack.Pop();
                text = memento.Text;
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                SaveStateToUndoStack();
                Memento memento = redoStack.Pop();
                text = memento.Text;
            }
        }

        private void SaveStateToUndoStack()
        {
            if (undoStack.Count == 256)
            {
                undoStack.TrimExcess();
            }
            undoStack.Push(new Memento(text));
        }

        private void SaveStateToRedoStack()
        {
            redoStack.Push(new Memento(text));
        }

        private void ClearRedoStack()
        {
            redoStack.Clear();
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            TextEditor editor = new TextEditor();

            editor.SetText("Привет, мир!");
            Console.WriteLine(editor.GetText()); 

            editor.SetText("Привет, всем!");
            Console.WriteLine(editor.GetText()); 

            editor.Undo();
            Console.WriteLine(editor.GetText()); 

            editor.Redo();
            Console.WriteLine(editor.GetText()); 
        }
    }
}
