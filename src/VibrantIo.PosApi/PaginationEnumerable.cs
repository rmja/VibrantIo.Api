using System.Runtime.CompilerServices;

namespace VibrantIo.PosApi;

internal class PaginationEnumerable<T>(Func<string?, CancellationToken, Task<PagedList<T>>> getPage)
    : IAsyncEnumerable<T>
    where T : IPaginateableObject
{
    private async IAsyncEnumerable<T> EnumerateAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        var page = await getPage(null, cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            string? lastId = null;
            foreach (var item in page.Data)
            {
                lastId = item.Id;
                yield return item;
            }

            if (!page.HasMore)
            {
                break;
            }

            page = await getPage(lastId, cancellationToken);
        }
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return EnumerateAsync(cancellationToken).GetAsyncEnumerator(cancellationToken);
    }
}
