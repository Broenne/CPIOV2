namespace CPIOngConfig
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// The root dispatcher fetcher.
    /// </summary>
    public class RootDispatcherFetcher
    {
        private static Dispatcher rootDispatcher;

        /// <summary>
        /// Gets the root dispatcher.
        /// </summary>
        /// <value>
        /// The root dispatcher.
        /// </value>
        public static Dispatcher RootDispatcher
        {
            get
            {
                try
                {
                    var curapp = Application.Current;
                        
                    var dis = Application.Current?.Dispatcher;
                    rootDispatcher = rootDispatcher ?? (curapp != null ? dis : rootDispatcher); // : new Dispatcher(Thread.CurrentThread));
                    return rootDispatcher;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            internal set
            {
                // unit tests can get access to this via InternalsVisibleTo
                rootDispatcher = value;
            }
        }
    }
}