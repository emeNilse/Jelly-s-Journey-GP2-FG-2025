using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace GraphViewTools
{
    [UxmlElement]
    public partial class RoomGraphView : GraphView
    {
        public Action<NodeView> OnNodeSelected;
        public HashSet<RoomData> RoomDataObjects = new HashSet<RoomData>();
        private RoomGraph _graph;

        public RoomGraphView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            GetRoomDataObjects();

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI Toolkit/Styles/Editor/RoomGraphEditor.uss");
            styleSheets.Add(styleSheet);
        }

        internal void PopulateView(RoomGraph roomGraph)
        {
            this._graph = roomGraph;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            foreach(RoomNode node in _graph.nodes)
            {
                CreateNodeView(node);
            }

            foreach(RoomNode node in _graph.nodes)
            {
                NodeView parentView = FindNodeView(node);
                List<EdgeData> outgoingEdges = _graph.GetOutgoingEdges(node);
                foreach(EdgeData edge in outgoingEdges)
                {
                    NodeView childView = GetNodeByGuid(edge.destinationGuid) as NodeView;
                    Port sourcePort = parentView.GetPortByName(edge.sourcePortName, parentView.OutputPorts);
                    Port destinationPort = childView.GetPortByName(edge.destinationPortName, childView.InputPorts);
                    if (sourcePort != null && destinationPort != null)
                    {
                        Edge newEdge = sourcePort.ConnectTo(destinationPort);
                        AddElement(newEdge);
                    }
                }
            }
        }

        private NodeView FindNodeView(RoomNode roomNode)
        {
            return GetNodeByGuid(roomNode.Guid) as NodeView;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange viewChange) 
        {
            if (viewChange.elementsToRemove != null)
            {
                foreach (GraphElement element in viewChange.elementsToRemove)
                {
                    NodeView nodeView =  element as NodeView;
                    if(nodeView != null)
                    {
                        _graph.DeleteNode(nodeView.Node);
                    }

                    Edge edge = element as Edge;
                    if(edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        _graph.RemoveEdge(parentView.Node, childView.Node, edge);
                    }
                }
            }

            if(viewChange.edgesToCreate != null)
            {
                foreach(Edge edge in viewChange.edgesToCreate)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _graph.AddEdge(parentView.Node, childView.Node, edge);
                }
            }
            return viewChange;
        }
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            var types = TypeCache.GetTypesDerivedFrom<RoomGraphNode>();
            foreach(var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
        }

        void CreateNode(System.Type type)
        {
            RoomNode node = _graph.CreateNode(type);
            CreateNodeView(node);
        }
        void CreateNodeView(RoomNode roomNode)
        {
            NodeView nodeView = new NodeView(roomNode, RoomDataObjects);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            //return base.GetCompatiblePorts(startPort, nodeAdapter);
            return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node
            ).ToList();
        }

        private void GetRoomDataObjects()
        {
            Debug.Log("SEARCHING FOR ROOM DATA SCRIPTABLE OBJECTS:");
            string[] roomDataGuids = AssetDatabase.FindAssets("t:RoomData");
            foreach (string roomDataGuid in roomDataGuids)
            {
                Debug.Log(roomDataGuid);
                RoomData room = AssetDatabase.LoadAssetAtPath<RoomData>(AssetDatabase.GUIDToAssetPath(roomDataGuid));
                if(room != null && room.Guid == "")
                {
                    room.Guid = GUID.Generate().ToString();
                }
                RoomDataObjects.Add(room);
                
            }
            Debug.Log("SEARCH FOR ROOM DATA COMPLETE");
        }
    }
}