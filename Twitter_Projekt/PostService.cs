using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class PostService
    {
        public static List<string> ListOfPosts { get; private set; } = new List<string>();

        public void AddPost(string post)
        {
            if (!string.IsNullOrWhiteSpace(post))
            {
                ListOfPosts.Add(post);
            }
            else
            {
                throw new ArgumentException("Inlägget kan inte vara tomt.");
            }
        }

        public void DeletePost(int postIndex)
        {
            if (postIndex >= 0 && postIndex < ListOfPosts.Count)
            {
                ListOfPosts.RemoveAt(postIndex);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Ogiltigt inläggsnummer.");
            }
        }

        public void EditPost(int postIndex, string newContent)
        {
            if (postIndex >= 0 && postIndex < ListOfPosts.Count && !string.IsNullOrWhiteSpace(newContent))
            {
                ListOfPosts[postIndex] = newContent;
            }
            else
            {
                throw new ArgumentException("Ogiltigt inläggsnummer eller tomt innehåll.");
            }
        }
    }
}
