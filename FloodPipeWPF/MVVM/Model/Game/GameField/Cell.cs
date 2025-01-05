using System.Numerics;

namespace FloodPipeWPF.MVVM.Model.Game.GameField
{
    public enum CellType
    {
        EMPTY,
        CROSS,
        STRAIGHT,
        CURVE,
        T_JUNCTION,
    }

    public enum CellState
    {
        EMPTY,
        FULL,
        SOURCE,
    }

    public class Cell
    {
        private CellType _type;
        private Vector2 _position;
        private CellState _cellState;
        private List<Vector2> _relativeConnections;
        private int _rotation;

        public Cell(CellType cellType, Vector2 position, int rotation = 0, CellState cellState = CellState.EMPTY)
        {
            _type = cellType;
            _position = position;
            _cellState = cellState;
            _rotation = rotation;

            CreateCellConnectionsOnCellType();
        }

        private void CreateCellConnectionsOnCellType()
        {
            _relativeConnections = new List<Vector2>();

            switch (_type)
            {
                case CellType.EMPTY:
                    break;
                case CellType.CROSS:
                    _relativeConnections.Add(new Vector2(0, 1));
                    _relativeConnections.Add(new Vector2(0, -1));
                    _relativeConnections.Add(new Vector2(1, 0));
                    _relativeConnections.Add(new Vector2(-1, 0));
                    break;
                case CellType.STRAIGHT:
                    _relativeConnections.Add(new Vector2(0, 1));
                    _relativeConnections.Add(new Vector2(0, -1));
                    break;
                case CellType.CURVE:
                    _relativeConnections.Add(new Vector2(0, 1));
                    _relativeConnections.Add(new Vector2(1, 0));
                    break;
                case CellType.T_JUNCTION:
                    _relativeConnections.Add(new Vector2(0, 1));
                    _relativeConnections.Add(new Vector2(1, 0));
                    _relativeConnections.Add(new Vector2(-1, 0));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public CellType Type
        {
            get => _type;
            set => _type = value;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public CellState CellState
        {
            get => _cellState;
            set => _cellState = value;
        }

        public List<Vector2> RelativeConnections
        {
            get => _relativeConnections;
        }

        public int Rotation
        {
            get => _rotation;
        }

        public void RotateClockwise()
        {
            _rotation = (_rotation + 1) % 4;
            RotateConnectionsClockwise();
        }

        public void RotateCounterClockwise()
        {
            _rotation = (_rotation - 1 + 4) % 4;
            RotateConnectionsCounterClockwise();
        }

        private void RotateConnectionsCounterClockwise()
        {
            for (int i = 0; i < _relativeConnections.Count; i++)
            {
                var connection = _relativeConnections[i];
                var temp = connection.X;
                connection.X = -connection.Y;
                connection.Y = temp;
                _relativeConnections[i] = connection;
            }
        }

        private void RotateConnectionsClockwise()
        {
            for (int i = 0; i < _relativeConnections.Count; i++)
            {
                var connection = _relativeConnections[i];
                var temp = connection.X;
                connection.X = connection.Y;
                connection.Y = -temp;
                _relativeConnections[i] = connection;
            }
        }
    }
}
