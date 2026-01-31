using UnityEngine;

public class GridMaker : MonoBehaviour
{
	[SerializeField] private int gridSize = 10;

	//void OnPostRender()
	//{
	//	CommandBuffer commandBuffer = new CommandBuffer();	

	//	mat.SetPass(0);

	//	GL.PushMatrix();
	//	GL.MultMatrix(transform.localToWorldMatrix);
	//	MeshTopology.Lines
	//	GL.Color(new Color(0.49f, 0.49f, 0.49f));
	//          GL.Begin(GL.LINES);

	//	for (int i = -gridSize; i < gridSize; i++)
	//	{
	//		for (int j = -gridSize; j < gridSize; j++)
	//		{
	//			GL.Vertex(new Vector3(-100, 0, j));
	//			GL.Vertex(new Vector3(100, 0, j));
	//		}
	//	}

	//	GL.End();
	//	GL.PopMatrix();
	//}

	static Material lineMaterial;

	static void CreateLineMaterial()
	{
		if (!lineMaterial)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt("_Cull", (int) UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt("_ZWrite", 0);
		}
	}

	// Will be called after all regular rendering is done
	public void OnRenderObject()
	{
			 

		CreateLineMaterial();
		// Apply the line material
		lineMaterial.SetPass(0);

		GL.PushMatrix();
		// Set transformation matrix for drawing to
		// match our transform
		GL.MultMatrix(transform.localToWorldMatrix);

		// Draw lines
		GL.Begin(GL.LINES);
		for (int i = -gridSize; i < gridSize; ++i)
		{
			GL.Color(i == 0 ? new Color(1f, 0f, 0f, 0.3f) : new Color(0.5f, 0.5f, 0.5f, 0.3f));

			GL.Vertex(new Vector3(-gridSize, 0, i));
			GL.Vertex(new Vector3(gridSize, 0, i));
		}

		for (int i = -gridSize; i < gridSize; ++i)
		{
			GL.Color(i == 0 ? new Color(0f, 1f, 0f, 0.3f) : new Color(0.5f, 0.5f, 0.5f, 0.3f));

			GL.Vertex(new Vector3(i, 0, -gridSize));
			GL.Vertex(new Vector3(i, 0, gridSize));
		}

		GL.End();
		GL.PopMatrix();
	}
	// private void OnPostRender()
	// {
	// 	for (int i = -gridSize; i < gridSize; i++)
	// 	{
	// 		for (int j = -gridSize; j < gridSize; j++)
	// 		{
	// 			GL.PushMatrix();
	// 			mat.SetPass(0);
	// 			GL.LoadOrtho();
	//
	// 			GL.Begin(GL.LINES);
	// 			GL.Color(Color.gray);
	// 			GL.Vertex(new Vector3(-100, 0, j));
	// 			GL.Vertex(new Vector3(100, 0, j));
	// 			GL.End();
	//
	// 			GL.PopMatrix();
	// 			// var children = new GameObject();
	// 			// children.transform.SetParent(transform);
	// 			// var horiz = children.AddComponent<LineRenderer>();
	// 			// horiz.SetPositions(new[] {new Vector3(-100, 0, j), new Vector3(100, 0, j)});
	// 			//
	// 			// children = new GameObject();
	// 			// children.transform.SetParent(transform);
	// 			// var vertical = children.AddComponent<LineRenderer>();
	// 			// vertical.SetPositions(new[] {new Vector3(j, 0, -100), new Vector3(j, 0, 100)});
	// 		}
	// 	}
	// }
}