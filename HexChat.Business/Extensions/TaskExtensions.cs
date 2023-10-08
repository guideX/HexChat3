/// <summary>
/// Task Extensions
/// </summary>
public static class TaskExtensions {
    /// <summary>
    /// Safe Fire and Forget
    /// </summary>
    /// <param name="task"></param>
    /// <param name="continueOnCapturedContext"></param>
    /// <param name="onException"></param>
    public static async void SafeFireAndForget(this Task task, bool continueOnCapturedContext = true, Action<Exception>? onException = null) {
        try {
            await task.ConfigureAwait(continueOnCapturedContext);
        } catch (Exception ex) when (onException != null) {
            onException(ex);
        }
    }
}