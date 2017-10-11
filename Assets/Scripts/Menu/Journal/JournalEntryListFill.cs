using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Journal
{
    public class JournalEntryListFill : MonoBehaviour
    {
        public GameObject buttonPrefab;
        public GameObject entryPanel;
        private JournalManager manager;

        void Start()
        {
            manager = GameObject.Find("ManagerGO").GetComponent<JournalManager>();
            manager.onEntryUnlocked.AddListener(() => RedrawList());
            RedrawList();
        }
        private void RedrawList()
        {
            foreach (Transform ch in this.transform)
                Destroy(ch.gameObject);

            if (manager == null)
                manager = GameObject.Find("ManagerGO").GetComponent<JournalManager>();

            for (int i = 0; i < manager.GetEntriesCount(); ++i )
            {
                AddButton(manager.GetEntry(i).title, manager.GetEntry(i).text, i);
            }
            ShowEntry(0);
        }

        

        private void AddButton(string buttonName, string screenText, int buttonGOName)
        {
            GameObject button = Instantiate(buttonPrefab, this.gameObject.transform);
            button.name = "" + buttonGOName;
            button.transform.Find("Text").GetComponent<Text>().text = buttonName;
            button.GetComponent<Button>().onClick.AddListener
               (() => ShowEntry(int.Parse(button.transform.name)));

        }
        public GameObject EntryText;
        private void ShowEntry(int i)
        {
            JournalEntry e = manager.GetEntry(i);
            entryPanel.transform.Find("TitleText").GetComponent<Text>().text = e.title;
            EntryText.GetComponent<Text>().text = e.text;
        }

        
    }
}
