// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------
using ClassLibrary;
namespace Training {
   internal class Program {
      static void Main (string[] args) { 
         TDEndQueue<int>mQ = new TDEndQueue<int>();
         mQ.EnqueueRear (1);
         mQ.EnqueueRear (2);
         mQ.EnqueueRear (3);
         mQ.EnqueueRear (4);
         //mQ.DisplayDeque ();
      }
   }
}