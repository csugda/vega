using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Menu.Journal
{
    [System.Serializable]
    public class JournalEntryUnlockedEvent : UnityEvent
    {
    }
    public class JournalManager : MonoBehaviour
    {
        const int EntriesPerArea = 8;
        const int AREAS = 6;


        public JournalEntryUnlockedEvent onEntryUnlocked;
        private JournalEntry[] entries;
        public bool[] unlocked;
        // Use this for initialization
        void Start()
        {
            entries = ReadJournalEntries();
            unlocked = new bool[entries.Length];
            /*TEMP*/ unlocked[0] = true;
        }

        public int GetEntriesCount()
        {
            return this.entries.Length;
        }

        private JournalEntry[] ReadJournalEntries()
        {
            JournalEntry[] temp = new JournalEntry[EntriesPerArea * AREAS];
            temp[0] = new JournalEntry { title = "one", text = "Hey, this actully worked!"};
            temp[1] = new JournalEntry { title = "two", text = "Entry Two Unlocked!\nCongrats, it worked."};
            for (int i = 2; i < EntriesPerArea * AREAS; ++i)
            {
                temp[i] = new JournalEntry { title = i.ToString(), text = "You Dont see anything :)" };
            }
            return temp;

        }

        public void UnlockNextJournalEntry (int area)
        {
            for (int i = area * EntriesPerArea; i < (area * EntriesPerArea) + EntriesPerArea; ++i)
            {
                if (unlocked[i] == false)
                {
                    unlocked[i] = true;
                    onEntryUnlocked.Invoke();
                    return;
                }
            }
        }

        private String ScrambledEntry(int len)
        {
            string res = "";
            for (int i = 1; i <= len; ++i)
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
            if (unlocked[i])
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
