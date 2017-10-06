using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu.Journal
{
    public class JournalManager : MonoBehaviour
    {
        private JournalEntry[] entries;
        // Use this for initialization
        void Start()
        {
            entries = ReadJournalEntries();
            Debug.Log(entries == null ? "entries is null" : "entries is not null");
        }

        public int GetEntriesCount()
        {
            return this.entries.Length;
        }

        private JournalEntry[] ReadJournalEntries()
        {
            JournalEntry[] temp = new JournalEntry[2];
            temp[0] = new JournalEntry { title = "one", text = "Hey, this actully worked!", unlocked = true };
            temp[1] = new JournalEntry { title = "two", text = "if you see this something is wrong", unlocked = false };
            return temp;

        }

        

        // Update is called once per frame
        void Update()
        {

        }

        private String ScrambledEntry(int len)
        {
            string res = "";
            for (int i = 0; i < len; ++i)
            {
                char c = ' ';
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        c = (char)UnityEngine.Random.Range(48, 57);
                        break;
                    case 1:
                        c = (char)UnityEngine.Random.Range(65, 90);
                        break;
                    case 2:
                        c = (char)UnityEngine.Random.Range(97, 122);
                        break;

                }
                res += c;
                if (i % 20 == 0)
                    res += UnityEngine.Random.Range(0, 3) == 0 ? "\n" : "";
            }

            return res;
        }

        public JournalEntry GetEntry(int i)
        {
            if (entries[i].unlocked)
                return entries[i];
            else
            {
                JournalEntry e = new JournalEntry
                {
                    title = ScrambledEntry(10),
                    text = ScrambledEntry(200)
                };
                return e;
            }
        }
    }
}
