using System.Runtime.InteropServices;
using TransferFromJsonToDatabaseToExcel.Models;
using Timer = System.Threading.Timer;

namespace TransferFromJsonToDatabaseToExcel.Classes;

public class CrudeService
{
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    private const byte VkScroll = 0x91;
    private const byte KeyeventfKeyup = 0x2;

    /// <summary>
    /// How long between intervals, currently 20 seconds
    /// </summary>
    public static int Interval = 500 * 1;
    private static Timer _workTimer;

    /// <summary>
    /// Text to display to listener 
    /// </summary>
    /// <param name="message">text</param>
    public delegate void MessageHandler(string message);
    /// <summary>
    /// Optional event 
    /// </summary>
    public static event MessageHandler Message;
    /// <summary>
    /// Flag to determine if timer should initialize 
    /// </summary>
    public static bool ShouldRun { get; set; } = true;

    public static bool DoDatabaseWork { get; set; }

    public static DataOperations DataOperations = new();
    public static List<Container> Containers { get; set; } = new();

    /// <summary>
    /// Default initializer
    /// </summary>
    private static void Initialize()
    {
        if (!ShouldRun) return;
        _workTimer = new Timer(Dispatcher);
        _workTimer.Change(Interval, Timeout.Infinite);
    }

    /// <summary>
    /// Initialize with time to delay before triggering <see cref="Worker"/>
    /// </summary>
    /// <param name="interval"></param>
    private static void Initialize(int interval)
    {
        if (!ShouldRun) return;
        Interval = interval;
        _workTimer = new Timer(Dispatcher);
        _workTimer.Change(Interval, Timeout.Infinite);
    }
    /// <summary>
    /// Trigger work, restart timer
    /// </summary>
    /// <param name="e"></param>
    private static void Dispatcher(object e)
    {
        Worker();
        _workTimer.Dispose();
        Initialize();
    }

    /// <summary>
    /// Start timer without a <see cref="Action"/>
    /// </summary>
    public static void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Stop timer
    /// </summary>
    public static void Stop(string text = "Stopped")
    {
        _workTimer.Dispose();
        Message?.Invoke(text);
    }

    /// <summary>
    /// This is where work is performed
    /// </summary>
    private static void Worker()
    {
        var container = FileOperations.Receive();
        if (container is not null)
        {
            Containers.Add(container);
            DoDatabaseWork = true;
        }
        else
        {
            if (DataOperations.AddRecords(Containers))
            {
                if (DoDatabaseWork)
                {
                    Console.Clear();
                    Message?.Invoke("[yellow]Added records to database[/]                     ");
                    DoDatabaseWork = false;
                    DataOperations.ExportFromDatabaseToExcel();
                    Console.Clear();
                    Message?.Invoke("[green]Exported from database to Excel[/]");
                    Stop();
                }
            }
            
        }

        PressScrollLock();
        PressScrollLock();
    }

    private static void PressScrollLock()
    {
        keybd_event(VkScroll, 0x45, 0, 0);
        keybd_event(VkScroll, 0x45, KeyeventfKeyup, 0);
    }
}