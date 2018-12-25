using System.Collections.Generic;

namespace Utils.Logic
{
    public class Iterators
    {
            /// <summary>
            /// Разбирает список ячеек таблицы на список строк той же таблицы.
            /// Каждая строка таблички - это список из string(содержимого ячеек таблицы)
            /// </summary>
            /// <param name="amountInLine">Количество столбцов страницы</param>
            /// <param name="table">Список ячеек таблицы слева направо, сверху вниз</param>
            /// <returns>Список строк</returns>
            public List<List<string>> TableIterateAllLines(int amountInLine, List<string> table)
            {
                var linesCount = table.Count / amountInLine;
                var rows = new List<List<string>>();
                for (var i = 0; i < linesCount; i++)
                {
                    rows.Add(Iterate(i, amountInLine, table));
                }

                return rows;
            }

            private List<string> Iterate(int lineIndex, int amountInLine, List<string> table)
            {
                var groupFirstIndex = lineIndex * amountInLine; // First element of group(row) by index
                var result = new List<string>();
                for (var i = 0; i < amountInLine; i++)
                {
                    result.Add(table[groupFirstIndex + i]);
                }

                return result;
            }
        }
    }