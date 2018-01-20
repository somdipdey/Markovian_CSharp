using System;
using System.Collections.Generic;
using System.Text;

namespace Markovian_CSharp
{
    // Author: Somdip Dey, 2018
    // Description: This library provides a base class for Markov Model 
    //for random text generation and prediction.
    public class MarkovWordModel
    {
        private int keyLength;
        private String[] computedText;
        private Random myRandom;

        // <summary>
        // Constructor for Markov Model class
        // </summary>
        public MarkovWordModel(int NumberOfWords)
        {
            keyLength = NumberOfWords;
            myRandom = new Random();
        }

        // <summary>
        // Get the computed training text split in words for the Markov Model
        // </summary>
        public String[] GetTrainingText()
        {
            return computedText;
        }

        // <summary>
        // Get the key length
        // </summary>
        public int GetKeyLength()
        {
            return keyLength;
        }

        // <summary>
        // SetRandom method sets the number of digits
        // to generate random index
        // </summary>
        public void SetRandom(int Seed)
        {
            myRandom = new Random(Seed);
        }

        // <summary>
        // Set the training text
        // </summary>
        public void SetTraining(String TrainingText)
        {
            computedText = TrainingText.Split(' ');
        }

        // <summary>
        // Predicts the next character by finding all the characters that follow a substring of keyLength
        // characters in the training text, and then randomly picking one of them as the next character.
        // </summary>
        public String GetRandomText(int NumberOfWords)
        {
            if (computedText == null)
            {
                throw new NullReferenceException("Training text is not initialised");
            }
            StringBuilder sb = new StringBuilder();
            // Generate a random index from valid indexes (those that have following characters)
            int index = myRandom.Next(computedText.Length - keyLength);
            // Assign to key the word at the random index that is keyLength long
            String[] key = new string[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                if (index + i < computedText.Length)
                {
                    key[i] = computedText[index + i];
                    sb.Append(key[i]);
                    sb.Append(' ');
                }
            }

            // NOTE: Generate numWords minus keyLength cuz they are set before the loop
            for (int k = 0; k < NumberOfWords - keyLength; k++)
            {
                // Find all words that follow the current word(s) string
                List<String> follows = GetFollows(key);
                // Break if no characters were found
                if (follows.Count == 0)
                {
                    break;
                }
                // Randomly pick one of them as the successor
                index = myRandom.Next(follows.Count);
                String successor = follows[index];
                sb.Append(successor);
                sb.Append(' ');

                // Combine old key (except first word) with successor to make next key
                String[] temp = new String[key.Length];
                for (int j = 0; j < key.Length; j++)
                {
                    if (j + 1 < key.Length)
                        temp[j] = key[j + 1];
                    else
                        temp[j] = successor;
                }
                temp.CopyTo(key, 0);
            }
            return sb.ToString();
        }


        // <summary>
        // Finds all the characters from the private variable computedText in MarkovOne that 
        // follow key and puts all these characters into an list of Strings.
        // @returns the List of String
        // </summary>
        public List<String> GetFollows(String[] key)
        {
            List<String> follows = new List<String>();
            // Loop through computedText until no more words are found
            int index = 0;
            while (true)
            {
                // Find indexes of key and succeeding character
                index = IndexOf(computedText, key, index);

                // Break if key isn't found or if successor index is greater than last index
                if (index == -1 || (index + 1) >= computedText.Length)
                {
                    break;
                }
                // Add to ArrayList the character immediately after key
                String successor = computedText[index + 1];
                follows.Add(successor);
                // Update search position in computedText
                index += 1;
            }
            return follows;
        }

        // Method to search index of target
        /**
         * Looks at the start position and returns the first index location in words that
         * matches target. If no word is found, then returns -1.
         */
        private int IndexOf(String[] words, String[] target, int start)
        {
            int index = -1;

            for (int i = start; i < words.Length - (target.Length - 1); i++)
            {
                int count = 0;
                int matched = 0;
                while (count < target.Length)
                {
                    if (words[i + count] == target[count])
                    {
                        matched++;
                    }
                    count++;
                }

                if (matched == target.Length)
                {
                    return i + target.Length;
                }
            }

            return index;
        }
    }
}
