using Training;

namespace TestQueue {
   [TestClass]
   public class TQueueTests {
      TQueue<int> tQueue = new ();
      Queue<int> queue = new ();
      [TestMethod]
      public void TestEnqueueDequeue () {
         tQueue.Enqueue (1);
         queue.Enqueue (1);
         Assert.AreEqual (tQueue[0], queue.ElementAt (0));
         tQueue.Enqueue (2);
         queue.Enqueue (2);
         Assert.AreEqual (tQueue.Peek (), queue.Peek ());
         Assert.AreEqual (tQueue.Dequeue (), queue.Dequeue ());
         Assert.AreEqual (tQueue.Peek (), queue.Peek ());
      }

      [TestMethod]
      public void TestPeek () {
         Assert.ThrowsException<InvalidOperationException> (() => tQueue.Peek ());
         tQueue.Enqueue (1);
         tQueue.Enqueue (2);
         tQueue.Enqueue (3);
         tQueue.Enqueue (4);
         Assert.AreEqual (1, tQueue.Peek ());
         tQueue.Dequeue ();
         Assert.AreEqual (2, tQueue.Peek ());
         tQueue.Enqueue (5);
         tQueue.Enqueue (6);
         Assert.AreEqual (2, tQueue.Peek ());
      }

      [TestMethod]
      public void TestIndexer () {
         TQueue<int> tQueue = new ();
         tQueue.Enqueue (1);
         tQueue.Enqueue (2);
         tQueue.Enqueue (3);
         tQueue.Enqueue (4);
         Assert.AreEqual (1, tQueue[0]);
         Assert.AreEqual (2, tQueue[1]);
         Assert.AreEqual (3, tQueue[2]);
         Assert.AreEqual (4, tQueue[3]);
         Assert.ThrowsException<IndexOutOfRangeException> (() => _ = tQueue[-1]);
         Assert.ThrowsException<IndexOutOfRangeException> (() => _ = tQueue[4]);
         tQueue[0] = 10;
         tQueue[3] = 40;
         Assert.AreEqual (10, tQueue[0]);
         Assert.AreEqual (40, tQueue[3]);
         Assert.ThrowsException<IndexOutOfRangeException> (() => tQueue[-1] = 99);
         Assert.ThrowsException<IndexOutOfRangeException> (() => tQueue[4] = 99);
      }


      [TestMethod]
      public void TestCount () {
         tQueue.Enqueue (1);
         queue.Enqueue (1);
         tQueue.Enqueue (2);
         queue.Enqueue (2);
         Assert.AreEqual (tQueue.Count (), queue.Count);
      }

      [TestMethod]
      public void TestModifyCapacity () {
         tQueue.Enqueue (1);
         tQueue.Enqueue (2);
         tQueue.Enqueue (3);
         tQueue.Enqueue (4);
         tQueue.Enqueue (5);
         Assert.AreEqual (8, tQueue.ModifyCapacity ());
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         Assert.AreEqual (4, tQueue.ModifyCapacity ());
         tQueue.Enqueue (6);
         Assert.AreEqual (4, tQueue.ModifyCapacity ());
         tQueue.Enqueue (7);
         Assert.AreEqual (8, tQueue.ModifyCapacity ());
      }

      [TestMethod]
      public void TestIsEmpty () {
         Assert.AreEqual (tQueue.IsEmpty, queue.Count == 0);
         tQueue.Enqueue (1);
         queue.Enqueue (1);
         Assert.AreEqual (tQueue.IsEmpty, queue.Count == 0);
      }

      [TestMethod]
      [ExpectedException (typeof (InvalidOperationException))]
      public void TestDequeueException () {
         Assert.ThrowsException<InvalidOperationException> (() => tQueue.Dequeue ());
      }

      [TestMethod]
      [ExpectedException (typeof (IndexOutOfRangeException))]
      public void TestIndexerException () => _ = tQueue[0];
   }
}