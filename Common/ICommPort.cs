namespace Common
{
    public interface ICommPort
    {
        bool IsOpen { get; }

        void Close();
        void Dispose();
        void InitializePort(string portName);
        void Open();
        string SendReceive(string data);
        void SendWrite(string data);
    }
}