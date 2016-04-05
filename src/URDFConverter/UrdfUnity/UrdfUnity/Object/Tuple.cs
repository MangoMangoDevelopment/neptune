namespace UrdfUnity.Object
{
    /// <summary>
    /// Represents a data structure with a pair of elements with specific types. Which also
    /// reflects the Tuple object in .Net Framework 4.x that doesn't exists in .Net Framework 3.5. 
    /// </summary>
    /// <typeparam name="T1">First element type in data structure</typeparam>
    /// <typeparam name="T2">Second element type in data structure</typeparam>
    public class Tuple <T1, T2>
    {
        public T1 Item1;
        public T2 Item2;

        public Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
    }

    /// <summary>
    /// Represents a data structure with a triple of elements with specific types. Which also
    /// reflects the Tuple object in .Net Framework 4.x that doesn't exists in .Net Framework 3.5. 
    /// </summary>
    /// <typeparam name="T1">First element type in data structure</typeparam>
    /// <typeparam name="T2">Second element type in data structure</typeparam>
    /// <typeparam name="T3">Third element type in data structure</typeparam>
    public class Tuple <T1, T2, T3>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public Tuple (T1 item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }
    }
}
