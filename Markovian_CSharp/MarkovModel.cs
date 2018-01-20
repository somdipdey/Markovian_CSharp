using System;
using System.Collections.Generic;
using System.Text;

namespace Markovian_CSharp
{
    // Author: Somdip Dey, 2018
    // Description: This library provides a base class for Markov Model 
    //for random text generation and prediction.
    public class MarkovModel
    {
        private int keyLength;
        private String computedText;
        private Random myRandom;

        // <summary>
        // Constructor for Markov Model class
        // </summary>
        public MarkovModel(int NumberOfCharacters)
        {
            keyLength = NumberOfCharacters;
            myRandom = new Random();
        }

        // <summary>
        // Get the computed training text for the Markov Model
        // </summary>
        public String GetTrainingText()
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
            computedText = TrainingText.Trim();
        }

        // <summary>
        // Predicts the next character by finding all the characters that follow a substring of keyLength
        // characters in the training text, and then randomly picking one of them as the next character.
        // </summary>
        public String GetRandomText(int NumberOfCharacters)
        {
            if (computedText == null)
            {
                throw new NullReferenceException("Training text is not initialised");
            }
            StringBuilder sb = new StringBuilder();
            // Generate a random index from valid indexes (those that have following characters)
            int index = myRandom.Next(computedText.Length - keyLength);
            // Assign to key the character string at the random index that is keyLength long
            String key = computedText.Substring(index, keyLength);
            sb.Append(key);
            // NOTE: Generate numChars minus four cuz they are set before the loop
            for (int k = 0; k < NumberOfCharacters - keyLength; k++)
            {
                // Find all characters that follow the current 4-character string
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
                // Combine old key (except first character) with successor to make next key
                key = key.Substring(key.Length - (keyLength - 1)) + successor;
            }
            return sb.ToString();
        }


        // <summary>
        // Finds all the characters from the private variable myText in MarkovOne that 
        // follow key and puts all these characters into an list of Strings.
        // @returns the List of String
        // </summary>
        public List<String> GetFollows(String key)
        {
            List<String> follows = new List<String>();
            // Loop through myText until no more characters are found
            int pos = 0;
            while (true)
            {
                // Find indexes of key and succeeding character
                int index = computedText.IndexOf(key, pos);
                int indexOfSuccessor = index + key.Length;
                // Break if key isn't found or if successor index is greater than last index
                if (index == -1 || indexOfSuccessor >= computedText.Length)
                {
                    break;
                }
                // Add to ArrayList the character immediately after key
                String successor = computedText.Substring(indexOfSuccessor, 1);
                follows.Add(successor);
                // Update search position in myText
                pos = index + 1;
            }
            return follows;
        }

    }
}
