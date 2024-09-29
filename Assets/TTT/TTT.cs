using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public enum PlayerOption
{
    NONE, //0
    X, // 1
    O // 2
}

public class TTT : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public int turns;
    [SerializeField] BoardView board;

    PlayerOption currentPlayer = PlayerOption.X;
    Cell[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[Columns, Rows];

        board.InitializeBoard(Columns, Rows);

        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Columns; j++)
            {
                cells[j, i] = new Cell();
                cells[j, i].current = PlayerOption.NONE;
            }
        }
    }

    public void MakeOptimalMove()
    {
        int randomNum = Random.Range(1, 9);
        Debug.Log(randomNum);
        switch (turns)
        {
            case 1: //puts X on a corner (randomly)
                if(randomNum == 1 || randomNum == 2)
                {
                    cells[0, 0].current = currentPlayer; //top left corner
                    board.UpdateCellVisual(0, 0, currentPlayer);
                    EndTurn();
                }
                else if(randomNum == 3 || randomNum == 4)
                {
                    cells[0, 2].current = currentPlayer; //bottom left corner
                    board.UpdateCellVisual(0, 2, currentPlayer);
                    EndTurn();
                }
                else if (randomNum == 5 || randomNum == 6)
                {
                    cells[2, 0].current = currentPlayer; //top right corner
                    board.UpdateCellVisual(2, 0, currentPlayer);
                    EndTurn();
                }
                else
                {
                    cells[2, 2].current = currentPlayer; //bottom right corner
                    board.UpdateCellVisual(2, 2, currentPlayer);
                    EndTurn();
                }
                break;
            case 2: // puts O in center if open , if not it puts O in a corner randomly
                if (cells[1, 1].current == PlayerOption.NONE)
                {
                    cells[1, 1].current = currentPlayer;
                    board.UpdateCellVisual(1, 1, currentPlayer);
                    EndTurn();
                }
                else
                {
                    if (randomNum == 1 || randomNum == 2)
                    {
                        cells[0, 0].current = currentPlayer; //top left corner
                        board.UpdateCellVisual(0, 0, currentPlayer);
                        EndTurn();
                    }
                    else if (randomNum == 3 || randomNum == 4)
                    {
                        cells[0, 2].current = currentPlayer; //bottom left corner
                        board.UpdateCellVisual(0, 2, currentPlayer);
                        EndTurn();
                    }
                    else if (randomNum == 5 || randomNum == 6)
                    {
                        cells[2, 0].current = currentPlayer; //top right corner
                        board.UpdateCellVisual(2, 0, currentPlayer);
                        EndTurn();
                    }
                    else
                    {
                        cells[2, 2].current = currentPlayer; //bottom right corner
                        board.UpdateCellVisual(2, 2, currentPlayer);
                        EndTurn();
                    }
                }
                break;
            case 3:
                if (cells[1, 1].current == PlayerOption.O)  // if O owns center...
                {
                    if (cells[0, 0].current == PlayerOption.X) //top left
                    {
                        cells[2,2].current = currentPlayer; //bottom right
                        board.UpdateCellVisual(2, 2, currentPlayer);
                        EndTurn();
                    }
                    else if (cells[0, 2].current == PlayerOption.X) //bottom left
                    {
                        cells[2, 0].current = currentPlayer; //top right
                        board.UpdateCellVisual(2, 0, currentPlayer);
                        EndTurn();
                    }
                    else if (cells[2, 0].current == PlayerOption.X) //top right
                    {
                        cells[0, 2].current = currentPlayer; //bottom left
                        board.UpdateCellVisual(0, 2, currentPlayer);
                        EndTurn();
                    }
                    else //bottom right
                    {
                        cells[0, 0].current = currentPlayer; //top left
                        board.UpdateCellVisual(0, 0, currentPlayer);
                        EndTurn();
                    }
                }
                else if ((cells[0, 1].current == PlayerOption.O || cells[1, 0].current == PlayerOption.O || cells[1, 2].current == PlayerOption.O || cells[2, 1].current == PlayerOption.O) && (cells[0, 0].current == PlayerOption.X || cells[0, 2].current == PlayerOption.X || cells[2, 0].current == PlayerOption.X || cells[2, 2].current == PlayerOption.X)) //left mid, top mid, bottom mid, right mid: in that order 
                {
                    cells[1,1].current = currentPlayer;
                    board.UpdateCellVisual(1, 1, currentPlayer);
                    EndTurn();
                } // if O is on sides AND X is on a corner...
                else if ((cells[0, 1].current == PlayerOption.X || cells[1, 0].current == PlayerOption.X || cells[1, 2].current == PlayerOption.X || cells[2, 1].current == PlayerOption.X) && (cells[0, 0].current == PlayerOption.O || cells[0, 2].current == PlayerOption.O || cells[2, 0].current == PlayerOption.O || cells[2, 2].current == PlayerOption.O))
                {
                    cells[1, 1].current = currentPlayer;
                    board.UpdateCellVisual(1, 1, currentPlayer);
                    EndTurn();
                } // X on sides, O in corners...
                else if ((cells[0,0].current == PlayerOption.X || cells[0, 2].current == PlayerOption.X || cells[2, 0].current == PlayerOption.X || cells[2, 2].current == PlayerOption.X) && (cells[0, 0].current == PlayerOption.O || cells[2, 0].current == PlayerOption.O || cells[0, 2].current == PlayerOption.O || cells[2, 2].current == PlayerOption.O)) // if X AND O are on corners...
                {
                    if (cells[0,0].current == PlayerOption.X) //top left
                    {
                        if (cells[0, 2].current == PlayerOption.O)
                        {
                            cells[2, 0].current = currentPlayer;
                            board.UpdateCellVisual(2, 0, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[2, 0].current == PlayerOption.O)
                        {
                            cells[0,2].current = currentPlayer;
                            board.UpdateCellVisual(0, 2, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[0, 2].current = currentPlayer;
                                board.UpdateCellVisual (0, 2, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2,0].current = currentPlayer;
                                board.UpdateCellVisual(2, 0, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                    else if (cells[0, 2].current == PlayerOption.X) //bottom left
                    {
                        if (cells[0, 0].current == PlayerOption.O)
                        {
                            cells[2, 2].current = currentPlayer;
                            board.UpdateCellVisual(2, 2, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[2, 2].current == PlayerOption.O)
                        {
                            cells[0, 0].current = currentPlayer;
                            board.UpdateCellVisual(0, 0, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[0, 0].current = currentPlayer;
                                board.UpdateCellVisual(0, 0, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2, 2].current = currentPlayer;
                                board.UpdateCellVisual(2, 2, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                    else if (cells[2, 0].current == PlayerOption.X) //top right
                    {
                        if (cells[0, 0].current == PlayerOption.O)
                        {
                            cells[2, 2].current = currentPlayer;
                            board.UpdateCellVisual(2, 2, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[2, 2].current == PlayerOption.O)
                        {
                            cells[0, 0].current = currentPlayer;
                            board.UpdateCellVisual(0, 0, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[0, 0].current = currentPlayer;
                                board.UpdateCellVisual(0, 0, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2, 2].current = currentPlayer;
                                board.UpdateCellVisual(2, 2, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                    else //bottom right
                    {
                        if (cells[0, 2].current == PlayerOption.O)
                        {
                            cells[2, 0].current = currentPlayer;
                            board.UpdateCellVisual(2, 0, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[2, 0].current == PlayerOption.O)
                        {
                            cells[0, 2].current = currentPlayer;
                            board.UpdateCellVisual(0, 2, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[0, 2].current = currentPlayer;
                                board.UpdateCellVisual(0, 2, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2, 0].current = currentPlayer;
                                board.UpdateCellVisual(2, 0, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                }
                else if ((cells[0, 1].current == PlayerOption.X || cells[1, 0].current == PlayerOption.X || cells[1, 2].current == PlayerOption.X || cells[2, 1].current == PlayerOption.X) && (cells[0, 1].current == PlayerOption.O || cells[1, 0].current == PlayerOption.O || cells[1, 2].current == PlayerOption.O || cells[2, 1].current == PlayerOption.O)) // if X AND O are on sides...
                {
                    if (cells[0,1].current == PlayerOption.X)
                    {
                        if (cells[1,0].current == PlayerOption.O)
                        {
                            cells[0, 0].current = PlayerOption.X;
                            board.UpdateCellVisual(0,0,currentPlayer);
                            EndTurn();
                        }
                        else if (cells[1,2].current == PlayerOption.O)
                        {
                            cells[0, 2].current = PlayerOption.X;
                            board.UpdateCellVisual(0, 2, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if(randomNum % 2 == 0)
                            {
                                cells[0,0].current = PlayerOption.X;
                                board.UpdateCellVisual(0,0,currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[0,2].current = PlayerOption.X;
                                board.UpdateCellVisual(0,2,currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                    else if (cells[1, 0].current == PlayerOption.X)
                    {
                        if (cells[0, 1].current == PlayerOption.O)
                        {
                            cells[0, 0].current = PlayerOption.X;
                            board.UpdateCellVisual(0, 0, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[2, 1].current == PlayerOption.O)
                        {
                            cells[2, 0].current = PlayerOption.X;
                            board.UpdateCellVisual(2, 0, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[0, 0].current = PlayerOption.X;
                                board.UpdateCellVisual(0, 0, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2, 0].current = PlayerOption.X;
                                board.UpdateCellVisual(2, 0, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                    else if (cells[2, 1].current == PlayerOption.X)
                    {
                        if (cells[1, 0].current == PlayerOption.O)
                        {
                            cells[2, 0].current = PlayerOption.X;
                            board.UpdateCellVisual(2, 0, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[1, 2].current == PlayerOption.O)
                        {
                            cells[2, 2].current = PlayerOption.X;
                            board.UpdateCellVisual(2, 2, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[2, 0].current = PlayerOption.X;
                                board.UpdateCellVisual(2, 0, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2, 2].current = PlayerOption.X;
                                board.UpdateCellVisual(2, 2, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                    else
                    {
                        if (cells[0, 1].current == PlayerOption.O)
                        {
                            cells[0, 2].current = PlayerOption.X;
                            board.UpdateCellVisual(0, 2, currentPlayer);
                            EndTurn();
                        }
                        else if (cells[2, 1].current == PlayerOption.O)
                        {
                            cells[2, 2].current = PlayerOption.X;
                            board.UpdateCellVisual(2, 2, currentPlayer);
                            EndTurn();
                        }
                        else
                        {
                            if (randomNum % 2 == 0)
                            {
                                cells[0, 2].current = PlayerOption.X;
                                board.UpdateCellVisual(0, 2, currentPlayer);
                                EndTurn();
                            }
                            else
                            {
                                cells[2, 2].current = PlayerOption.X;
                                board.UpdateCellVisual(2, 2, currentPlayer);
                                EndTurn();
                            }
                        }
                    }
                }
                break;
            case 4:
                if (cells[1,1].current == PlayerOption.O && ((cells[0,0].current == PlayerOption.X && cells[2,2].current == PlayerOption.X) || (cells[0,2].current == PlayerOption.X && cells[2,0].current == PlayerOption.X))) 
                {
                    if (randomNum == 1 || randomNum == 2)
                    {
                        cells[1, 0].current = currentPlayer;
                        board.UpdateCellVisual(1,0,currentPlayer);
                        EndTurn();
                    }
                    else if (randomNum == 3 || randomNum == 4)
                    {
                        cells[2,1].current = currentPlayer;
                        board.UpdateCellVisual (2, 1, currentPlayer);
                        EndTurn();
                    }
                    else if (randomNum == 5 || randomNum == 6)
                    {
                        cells[1, 2].current = currentPlayer;
                        board.UpdateCellVisual(1,2,currentPlayer);
                        EndTurn();
                    }
                    else
                    {
                        cells[0, 1].current = currentPlayer;
                        board.UpdateCellVisual(0, 1, currentPlayer);
                        EndTurn();
                    }
                }
                else if (cells[0,0].current == PlayerOption.X && cells[1,0].current == PlayerOption.X && cells[2,0].current == PlayerOption.NONE)
                {
                    cells[2, 0].current = currentPlayer;
                    board.UpdateCellVisual(2,0,currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 0].current == PlayerOption.X && cells[1, 0].current == PlayerOption.NONE && cells[2, 0].current == PlayerOption.X)
                {
                    cells[1,0].current = currentPlayer;
                    board.UpdateCellVisual(1,0,currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 0].current == PlayerOption.NONE && cells[1, 0].current == PlayerOption.X && cells[2, 0].current == PlayerOption.X)
                {
                    cells[0, 0].current = currentPlayer;
                    board.UpdateCellVisual(0, 0, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 1].current == PlayerOption.NONE && cells[1, 1].current == PlayerOption.X && cells[2, 1].current == PlayerOption.X)
                {
                    cells[0, 1].current = currentPlayer;
                    board.UpdateCellVisual(0, 1, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 1].current == PlayerOption.X && cells[1, 1].current == PlayerOption.NONE && cells[2, 1].current == PlayerOption.X)
                {
                    cells[1, 1].current = currentPlayer;
                    board.UpdateCellVisual(1, 1, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 1].current == PlayerOption.X && cells[1, 1].current == PlayerOption.X && cells[2, 1].current == PlayerOption.NONE)
                {
                    cells[2, 1].current = currentPlayer;
                    board.UpdateCellVisual(2, 1, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 2].current == PlayerOption.NONE && cells[1, 2].current == PlayerOption.X && cells[2, 2].current == PlayerOption.X)
                {
                    cells[0, 2].current = currentPlayer;
                    board.UpdateCellVisual(0, 2, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 2].current == PlayerOption.X && cells[1, 2].current == PlayerOption.NONE && cells[2, 2].current == PlayerOption.X)
                {
                    cells[1, 2].current = currentPlayer;
                    board.UpdateCellVisual(1, 2, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 2].current == PlayerOption.X && cells[1, 2].current == PlayerOption.X && cells[2, 2].current == PlayerOption.NONE)
                {
                    cells[2, 2].current = currentPlayer;
                    board.UpdateCellVisual(2, 2, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 0].current == PlayerOption.NONE && cells[0, 1].current == PlayerOption.X && cells[0, 2].current == PlayerOption.X)
                {
                    cells[0, 0].current = currentPlayer;
                    board.UpdateCellVisual(0, 0, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 0].current == PlayerOption.X && cells[0, 1].current == PlayerOption.NONE && cells[0, 2].current == PlayerOption.X)
                {
                    cells[0, 1].current = currentPlayer;
                    board.UpdateCellVisual(0, 1, currentPlayer);
                    EndTurn();
                }
                else if (cells[0, 0].current == PlayerOption.X && cells[0, 1].current == PlayerOption.X && cells[0, 2].current == PlayerOption.NONE)
                {
                    cells[0, 2].current = currentPlayer;
                    board.UpdateCellVisual(0, 2, currentPlayer);
                    EndTurn();
                }
                else if (cells[1, 0].current == PlayerOption.X && cells[1, 1].current == PlayerOption.X && cells[1, 2].current == PlayerOption.NONE)
                {
                    cells[1, 2].current = currentPlayer;
                    board.UpdateCellVisual(1, 2, currentPlayer);
                    EndTurn();
                }
                else if (cells[1, 0].current == PlayerOption.X && cells[1, 1].current == PlayerOption.NONE && cells[1, 2].current == PlayerOption.X)
                {
                    cells[1, 1].current = currentPlayer;
                    board.UpdateCellVisual(1, 1, currentPlayer);
                    EndTurn();
                }
                else if (cells[1, 0].current == PlayerOption.NONE && cells[1, 1].current == PlayerOption.X && cells[1, 2].current == PlayerOption.X)
                {
                    cells[1, 0].current = currentPlayer;
                    board.UpdateCellVisual(1, 0, currentPlayer);
                    EndTurn();
                }
                else if (cells[2, 0].current == PlayerOption.X && cells[2, 1].current == PlayerOption.X && cells[2, 2].current == PlayerOption.NONE)
                {
                    cells[2, 2].current = currentPlayer;
                    board.UpdateCellVisual(2, 2, currentPlayer);
                    EndTurn();
                }
                else if (cells[2, 0].current == PlayerOption.X && cells[2, 1].current == PlayerOption.NONE && cells[2, 2].current == PlayerOption.X)
                {
                    cells[2, 1].current = currentPlayer;
                    board.UpdateCellVisual(2, 1, currentPlayer);
                    EndTurn();
                }
                else if (cells[2, 0].current == PlayerOption.NONE && cells[2, 1].current == PlayerOption.X && cells[2, 2].current == PlayerOption.X)
                {
                    cells[2, 2].current = currentPlayer;
                    board.UpdateCellVisual(2, 2, currentPlayer);
                    EndTurn();
                }
                break;
        }
    }

    public void ChooseSpace(int column, int row)
    {
        // can't choose space if game is over
        if (GetWinner() != PlayerOption.NONE)
            return;

        // can't choose a space that's already taken
        if (cells[column, row].current != PlayerOption.NONE)
            return;

        // set the cell to the player's mark
        cells[column, row].current = currentPlayer;

        // update the visual to display X or O
        board.UpdateCellVisual(column, row, currentPlayer);

        // if there's no winner, keep playing, otherwise end the game
        if(GetWinner() == PlayerOption.NONE)
            EndTurn();
        else
        {
            Debug.Log("GAME OVER!");
        }
    }

    public void EndTurn()
    {
        // increment player, if it goes over player 2, loop back to player 1
        currentPlayer += 1;
        turns += 1;
        if ((int)currentPlayer > 2)
            currentPlayer = PlayerOption.X;
    }

    public PlayerOption GetWinner()
    {
        // sum each row/column based on what's in each cell X = 1, O = -1, blank = 0
        // we have a winner if the sum = 3 (X) or -3 (O)
        int sum = 0;

        // check rows
        for (int i = 0; i < Rows; i++)
        {
            sum = 0;
            for (int j = 0; j < Columns; j++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check columns
        for (int j = 0; j < Columns; j++)
        {
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check diagonals
        // top left to bottom right
        sum = 0;
        for(int i = 0; i < Rows; i++)
        {
            int value = 0;
            if (cells[i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        // top right to bottom left
        sum = 0;
        for (int i = 0; i < Rows; i++)
        {
            int value = 0;

            if (cells[Columns - 1 - i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[Columns - 1 - i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        return PlayerOption.NONE;
    }
}
