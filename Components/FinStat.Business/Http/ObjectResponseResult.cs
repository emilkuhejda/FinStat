namespace FinStat.Business.Http
{
    public readonly struct ObjectResponseResult<T>
    {
        public ObjectResponseResult(T responseObject, string responseText)
        {
            Result = responseObject;
            Text = responseText;
        }

        public T Result { get; }

        public string Text { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is ObjectResponseResult<T>))
                return false;

            return Equals((ObjectResponseResult<T>)obj);
        }

        public bool Equals(ObjectResponseResult<T> other)
        {
            return Result.Equals(other.Result);
        }

        public override int GetHashCode()
        {
            return Result.GetHashCode() ^ Result.GetHashCode();
        }

        public static bool operator ==(ObjectResponseResult<T> left, ObjectResponseResult<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ObjectResponseResult<T> left, ObjectResponseResult<T> right)
        {
            return !(left == right);
        }
    }
}
