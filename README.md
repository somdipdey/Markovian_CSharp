# Markovian_CSharp
Markov Model (N characters) library written in CSharp [C#]

[![Build Status](https://travis-ci.org/somdipdey/Markovian_CSharp.svg?branch=master)](https://travis-ci.org/somdipdey/Markovian_CSharp)

## Using Markov Model in C#

            // Training text in string format
            String st = "I completed Hack Reactor in July 2016 and took almost 3 months before accepting an offer with Radius Intelligence.\n"+
                " I applied to 291 companies, did 32 phone screens, 16 technical screens, 13 coding challenges, 11 on-sites, and received 8 offers.\n" +
                "The offers ranged from $60-125k in salary from companies all over the US, and for both front end and full stack roles. In total, 2.8% of applications became offers.\n" + 
                "Here are 5 things I wish I’d known before I began my job search.";

            // Replace any new line with blank spaces to generate random text
            st = st.Replace('\n', ' ');

            // Create a new MarkovModel object, initialized with a keyLength of 6
            MarkovModel markov = new MarkovModel(6);

            // Train Markov model with the input text
            markov.SetTraining(st);

            // Set the seed of the random number generator
            markov.SetRandom(1);

            // Print out four random texts
            for (int k = 0; k < 4; k++)
            {
                String text = markov.GetRandomText(500);
                Console.Write(text + Environment.NewLine);
            }
            
            //Output::
            /*
            I applied to 291 companies, did 32 phone screens, 13 coding challenges, 11 on-sites, and received 8 offer with Radius Intelligence.  I applied to 291 companies, did 32 phone screens, 16 technical screens, 16 technical screens, 16 technical screens, 16 technical screens, 16 technical screens, 16 technical screens, 16 technical screens, 13 coding challenges, 11 on-sites, and for both front end and full stack roles. In total, 2.8% of applications became offers. Here are 5 things I wish I’d known be

            ons became offer with Radius Intelligence.  I applications became offers ranged from companies, did 32 phone screens, 13 coding challenges, 11 on-sites, and received 8 offer with Radius Intelligence.  I applications became offers. The offers ranged from companies all over the US, and for both front end and for both front end and for both front end and full stack roles. In total, 2.8% of applied to 291 companies, did 32 phone screens, 16 technical screens, 13 coding challenges, 11 on-sites, and r

            ted Hack Reactor in July 2016 and took almost 3 months before accepting an offer with Radius Intelligence.  I applications became offers. Here are 5 things I wish I’d known before I began my job search.

            st 3 months before I began my job search.
            */
