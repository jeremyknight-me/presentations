# Benchmarks

## GetById

|                    Method |     Mean |   Error |  StdDev | Allocated |
|-------------------------- |---------:|--------:|--------:|----------:|
|                       Ado | 594.5 us | 4.35 us | 3.64 us |   2.86 KB |
|         EF_FirstOrDefault | 621.8 us | 6.14 us | 5.75 us |   5.86 KB |
| EF_FirstOrDefault_NoTrack | 627.3 us | 5.46 us | 4.56 us |   6.49 KB |


## GetList

|            Method |     Mean |   Error |  StdDev |   Gen0 | Allocated |
|------------------ |---------:|--------:|--------:|-------:|----------:|
|               Ado | 605.4 us | 6.86 us | 6.08 us |      - |   4.69 KB |
|         EF_ToList | 645.7 us | 6.91 us | 6.47 us |      - |  14.45 KB |
| EF_ToList_NoTrack | 651.0 us | 7.59 us | 7.10 us | 0.9766 |  16.31 KB |

## GetMultiple

|            Method |       Mean |    Error |   StdDev | Allocated |
|------------------ |-----------:|---------:|---------:|----------:|
|               Ado |   616.5 us |  5.74 us |  5.09 us |    6.9 KB |
|         EF_ToList | 1,221.5 us | 12.09 us | 10.72 us |  15.03 KB |
| EF_ToList_NoTrack | 1,248.8 us | 23.72 us | 22.19 us |  17.97 KB |
