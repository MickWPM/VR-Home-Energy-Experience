using UnityEngine;

public class WiringVisualiser : MonoBehaviour
{
    public PowerConsumer consumer;
    public Transform wiringStartPoint;
    public Transform[] wirePath;
    public bool getPathFromChildren = false;
    public CircuitConnectionsNode connectedNode;

    [SerializeField] private LineRenderer lr;

    private void Awake()
    {
        if (wiringStartPoint == null) wiringStartPoint = transform;
        if (getPathFromChildren && transform.childCount > 0)
        {
            wirePath = new Transform[transform.childCount];
            for (int i = 0; i < wirePath.Length; i++)
            {
                wirePath[i] = transform.GetChild(i);
            }
        }
    }

    private void Update()
    {
        UpdateCircuitConnection();
    }

    private void UpdateCircuitConnection()
    {
        //get connected circuit for consumer?
        PowerCircuit connectedCircuit = connectedNode.CircuitRewirer.powerSource.GetCircuitByConsumer(consumer);
        var endPoint = connectedNode.GetCircuitConnectionNode(connectedCircuit).position;
        int numPathNodes = (wirePath == null || wirePath.Length == 0) ? 0 : wirePath.Length;
        Vector3[] positions = new Vector3[2 + numPathNodes];
        positions[0] = wiringStartPoint.position;
        for (int i = 0; i < numPathNodes; i++)
        {
            positions[i + 1] = wirePath[i].position;
        }
        positions[positions.Length - 1] = endPoint;

        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }
}
