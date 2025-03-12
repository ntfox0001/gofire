using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveEditor : Editor
{
    BezierCurve curve;
    SerializedProperty resolutionProp;
    SerializedProperty closeProp;
    SerializedProperty pointsProp;
    SerializedProperty colorProp;
    SerializedProperty sizeProp;
    private SerializedProperty _frozenXProp;
    private SerializedProperty _frozenYProp;
    private SerializedProperty _frozenZProp;

    private static bool showPoints = false;

    void OnEnable()
    {
        curve = (BezierCurve)target;

        resolutionProp = serializedObject.FindProperty("resolution");
        closeProp = serializedObject.FindProperty("_close");
        pointsProp = serializedObject.FindProperty("points");
        colorProp = serializedObject.FindProperty("drawColor");
        sizeProp = serializedObject.FindProperty("drawSize");
        _frozenXProp = serializedObject.FindProperty("frozenX");
        _frozenYProp = serializedObject.FindProperty("frozenY");
        _frozenZProp = serializedObject.FindProperty("frozenZ");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("adjust pos at head"))
        {
            ResetAtHead();
        }
        if (GUILayout.Button("adjust pos at center"))
        {
            ResetCenter();
        }
        if (GUILayout.Button("adjust pos at tail"))
        {
            ResetAtTail();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(sizeProp);
        EditorGUILayout.PropertyField(resolutionProp);
        EditorGUILayout.PropertyField(closeProp);
        EditorGUILayout.PropertyField(colorProp);
        
        GUILayout.Space(10);
        
        EditorGUILayout.PropertyField(_frozenXProp);
        EditorGUILayout.PropertyField(_frozenYProp);
        EditorGUILayout.PropertyField(_frozenZProp);
        
        GUILayout.Space(10);
        showPoints = EditorGUILayout.Foldout(showPoints, "Points");

        if (showPoints)
        {
            int pointCount = pointsProp.arraySize;

            for (int i = 0; i < pointCount; i++)
            {
                DrawPointInspector(curve[i], i);
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Point"))
            {
                AddPoint();
            }

            if (GUILayout.Button("Revert Point"))
            {
                curve.RevertPoint();
            }
            GUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }

    void AddPoint()
    {
        var lastPos = curve.pointCount > 0 ? curve.GetPos(curve[curve.pointCount - 1].localPosition + Vector3.one) : Vector3.zero; 
        GameObject pointObject = new GameObject("Point " + pointsProp.arraySize);
        pointObject.transform.parent = curve.transform;
        pointObject.transform.localPosition = Vector3.zero;
        BezierPoint newPoint = pointObject.AddComponent<BezierPoint>();

        Undo.RegisterCreatedObjectUndo(pointObject, "Add Point");

        newPoint.curve = curve;
        newPoint.localPosition = lastPos;
        newPoint.handle1 = Vector3.right * 1.0f;
        newPoint.handle2 = -Vector3.right * 1.0f;

        pointsProp.InsertArrayElementAtIndex(pointsProp.arraySize);
        pointsProp.GetArrayElementAtIndex(pointsProp.arraySize - 1).objectReferenceValue = newPoint;
    }

    void OnSceneGUI()
    {
        for (int i = 0; i < curve.pointCount; i++)
        {
            DrawPointSceneGUI(curve, curve[i]);
        }
    }

    void DrawPointInspector(BezierPoint point, int index)
    {
        SerializedObject serObj = new SerializedObject(point);

        SerializedProperty handleStyleProp = serObj.FindProperty("handleStyle");
        SerializedProperty handle1Prop = serObj.FindProperty("_handle1");
        SerializedProperty handle2Prop = serObj.FindProperty("_handle2");

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("X", GUILayout.Width(20)))
        {
            pointsProp.MoveArrayElement(curve.GetPointIndex(point), curve.pointCount - 1);
            pointsProp.arraySize--;
            DestroyImmediate(point.gameObject);
            return;
        }

        EditorGUILayout.ObjectField(point.gameObject, typeof(GameObject), true);

        if (index != 0 && GUILayout.Button(@"↑", GUILayout.Width(25)))
        {
            UnityEngine.Object other = pointsProp.GetArrayElementAtIndex(index - 1).objectReferenceValue;
            pointsProp.GetArrayElementAtIndex(index - 1).objectReferenceValue = point;
            pointsProp.GetArrayElementAtIndex(index).objectReferenceValue = other;
        }

        if (index != pointsProp.arraySize - 1 && GUILayout.Button(@"↓", GUILayout.Width(25)))
        {
            UnityEngine.Object other = pointsProp.GetArrayElementAtIndex(index + 1).objectReferenceValue;
            pointsProp.GetArrayElementAtIndex(index + 1).objectReferenceValue = point;
            pointsProp.GetArrayElementAtIndex(index).objectReferenceValue = other;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel++;
        EditorGUI.indentLevel++;

        int newType = (int)((object)EditorGUILayout.EnumPopup("Handle Type", (BezierPoint.HandleStyle)handleStyleProp.enumValueIndex));

        if (newType != handleStyleProp.enumValueIndex)
        {
            handleStyleProp.enumValueIndex = newType;
            if (newType == 0)
            {
                if (handle1Prop.vector3Value != Vector3.zero) handle2Prop.vector3Value = -handle1Prop.vector3Value;
                else if (handle2Prop.vector3Value != Vector3.zero) handle1Prop.vector3Value = -handle2Prop.vector3Value;
                else
                {
                    handle1Prop.vector3Value = new Vector3(0.1f, 0, 0);
                    handle2Prop.vector3Value = new Vector3(-0.1f, 0, 0);
                }
            }

            else if (newType == 1)
            {
                if (handle1Prop.vector3Value == Vector3.zero && handle2Prop.vector3Value == Vector3.zero)
                {
                    handle1Prop.vector3Value = new Vector3(0.1f, 0, 0);
                    handle2Prop.vector3Value = new Vector3(-0.1f, 0, 0);
                }
            }

            else if (newType == 2)
            {
                handle1Prop.vector3Value = Vector3.zero;
                handle2Prop.vector3Value = Vector3.zero;
            }
        }

        Vector3 newPointPos = EditorGUILayout.Vector3Field("Position : ", point.transform.localPosition);
        if (newPointPos != point.transform.localPosition)
        {
            Undo.RegisterCompleteObjectUndo(point.transform, "Move Bezier Point");
            point.transform.localPosition = newPointPos;
        }

        if (handleStyleProp.enumValueIndex == 0)
        {
            Vector3 newPosition;

            newPosition = EditorGUILayout.Vector3Field("Handle 1", handle1Prop.vector3Value);
            if (newPosition != handle1Prop.vector3Value)
            {
                // handle1Prop.vector3Value = newPosition;
                // handle2Prop.vector3Value = -newPosition;
                handle1Prop.vector3Value = curve.GetPos(newPosition);
                handle1Prop.vector3Value = curve.GetPos(-newPosition);
            }

            newPosition = EditorGUILayout.Vector3Field("Handle 2", handle2Prop.vector3Value);
            if (newPosition != handle2Prop.vector3Value)
            {
                // handle1Prop.vector3Value = -newPosition;
                // handle2Prop.vector3Value = newPosition;
                handle1Prop.vector3Value = curve.GetPos(-newPosition);
                handle1Prop.vector3Value = curve.GetPos(newPosition);
            }
        }

        else if (handleStyleProp.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(handle1Prop);
            EditorGUILayout.PropertyField(handle2Prop);
        }

        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;

        if (GUI.changed)
        {
            serObj.ApplyModifiedProperties();
            EditorUtility.SetDirty(serObj.targetObject);
        }
    }

    void ResetCenter()
    {
        var bounds = new Bounds();
        for (var i = 0; i < curve.pointCount; i++)
        {
            if (i == 0)
            {
                bounds.center = curve[i].position;
            }
            else
            {
                bounds.Encapsulate(curve[i].position);    
            }
            
        }
        
        var offset = curve.transform.position - bounds.center; 
        curve.transform.position = bounds.center;
        
        for (var i = 0; i < curve.pointCount; i++)
        {
            curve[i].transform.position += offset;
        }
    }

    void ResetAtHead()
    {
        if (curve.pointCount == 0) return;
        var offset = curve.transform.position - curve[0].position;
        curve.transform.position = curve[0].position = curve[0].position;
        for (var i = 0; i < curve.pointCount; i++)
        {
            curve[i].transform.position += offset;
        }
    }
    
    void ResetAtTail()
    {
        if (curve.pointCount == 0) return;
        var offset = curve.transform.position - curve[curve.pointCount-1].position;
        curve.transform.position = curve[curve.pointCount-1].position = curve[curve.pointCount-1].position;
        for (var i = 0; i < curve.pointCount; i++)
        {
            curve[i].transform.position += offset;
        }
    }
    
    private static void DrawPointSceneGUI(BezierCurve curve, BezierPoint point)
    {
        Handles.Label(point.position + new Vector3(0, HandleUtility.GetHandleSize(point.position) * 0.4f, 0), point.Name);

        Handles.color = Color.green;
        Vector3 newPosition = curve.GetPos(Handles.FreeMoveHandle(point.position,
            HandleUtility.GetHandleSize(point.position) * 0.1f * point.curve.drawSize,
            Vector3.zero,
            Handles.CubeHandleCap));

        if (newPosition != point.position)
        {
            Undo.RegisterCompleteObjectUndo(point.transform, "Move Point");
            point.transform.position = newPosition;
        }

        if (point.handleStyle != BezierPoint.HandleStyle.None)
        {
            Handles.color = Color.cyan;
            Vector3 newGlobal1 = curve.GetPos(Handles.FreeMoveHandle(point.globalHandle1,
                HandleUtility.GetHandleSize(point.globalHandle1) * 0.075f * point.curve.drawSize, Vector3.zero,
                Handles.SphereHandleCap));
            if (point.globalHandle1 != newGlobal1)
            {
                Undo.RegisterCompleteObjectUndo(point, "Move Handle");
                point.globalHandle1 = newGlobal1;
                if (point.handleStyle == BezierPoint.HandleStyle.Connected) point.globalHandle2 = -(newGlobal1 - point.position) + point.position;
            }

            Vector3 newGlobal2 = curve.GetPos(Handles.FreeMoveHandle(point.globalHandle2,
                HandleUtility.GetHandleSize(point.globalHandle2) * 0.075f * point.curve.drawSize, Vector3.zero,
                Handles.SphereHandleCap));
            if (point.globalHandle2 != newGlobal2)
            {
                Undo.RegisterCompleteObjectUndo(point, "Move Handle");
                point.globalHandle2 = newGlobal2;
                if (point.handleStyle == BezierPoint.HandleStyle.Connected) point.globalHandle1 = -(newGlobal2 - point.position) + point.position;
            }

            Handles.color = Color.yellow;
            Handles.DrawLine(point.position, point.globalHandle1, point.curve.drawSize * 1.5f);
            Handles.DrawLine(point.position, point.globalHandle2, point.curve.drawSize * 1.5f);
        }
    }

    public static void DrawOtherPoints(BezierCurve curve, BezierPoint caller)
    {
        foreach (BezierPoint p in curve.GetAnchorPoints())
        {
            if (p != caller) DrawPointSceneGUI(curve, p);
        }
    }

    [MenuItem("GameObject/Create Other/Bezier Curve")]
    public static void CreateCurve(MenuCommand command)
    {
        GameObject curveObject = new GameObject("BezierCurve");
        Undo.RegisterCompleteObjectUndo(curveObject, "Undo Create Curve");
        BezierCurve curve = curveObject.AddComponent<BezierCurve>();

        BezierPoint p1 = curve.AddPointAt(Vector3.forward * 0.5f);
        p1.handleStyle = BezierPoint.HandleStyle.Connected;
        p1.handle1 = new Vector3(-0.28f, 0, 0);

        BezierPoint p2 = curve.AddPointAt(Vector3.right * 0.5f);
        p2.handleStyle = BezierPoint.HandleStyle.Connected;
        p2.handle1 = new Vector3(0, 0, 0.28f);

        BezierPoint p3 = curve.AddPointAt(-Vector3.forward * 0.5f);
        p3.handleStyle = BezierPoint.HandleStyle.Connected;
        p3.handle1 = new Vector3(0.28f, 0, 0);

        BezierPoint p4 = curve.AddPointAt(-Vector3.right * 0.5f);
        p4.handleStyle = BezierPoint.HandleStyle.Connected;
        p4.handle1 = new Vector3(0, 0, -0.28f);

        curve.close = true;
    }
}
