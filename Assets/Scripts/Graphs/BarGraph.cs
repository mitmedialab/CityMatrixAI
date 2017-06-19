namespace UnityEngine.UI {

public class BarGraph : MaskableGraphic {

	[Range(0f, 1f)]
	public float val = 0.5f;

	public Color fill;
	public Color outline;
	public Color highlight;

	[Range(0f, 1f)]
	public float otherOffset = 0.1f;
	public float[] otherVals;
	public Color[] otherCols;

	public float lineWidth = 0.25f;
	public float highlightWidth = 0.5f;


	protected override void OnPopulateMesh(VertexHelper vh) {
		Vector2 size = this.GetComponent<RectTransform>().rect.size / 2;
		float height = (val - 0.5f) * 2 * size.y;
		vh.Clear();

		vh.AddUIVertexQuad(makeQuad(-size, new Vector2(lineWidth - size.x, size.y), outline));
		vh.AddUIVertexQuad(makeQuad(new Vector2(-size.x, size.y - lineWidth), size, outline));
		vh.AddUIVertexQuad(makeQuad(new Vector2(size.x - lineWidth, -size.y), size, outline));
		vh.AddUIVertexQuad(makeQuad(-size, new Vector2(size.x, lineWidth - size.y), outline));

		vh.AddUIVertexQuad(makeQuad(-size, new Vector2(size.x, height), fill));

		vh.AddUIVertexQuad(makeQuad(-size, new Vector2(highlightWidth - size.x, height), highlight));
		vh.AddUIVertexQuad(makeQuad(new Vector2(-size.x, height - highlightWidth), new Vector2(size.x, height), highlight));
		vh.AddUIVertexQuad(makeQuad(new Vector2(size.x - highlightWidth, -size.y), new Vector2(size.x, height), highlight));
		vh.AddUIVertexQuad(makeQuad(-size, new Vector2(size.x, highlightWidth - size.y), highlight));

		float offset = size.x * otherOffset;
		for(int i = 0; i < otherVals.Length; i ++) {
			height = (otherVals[i] - 0.5f) * 2 * size.y;
			Color col = otherCols[i] == null ? Color.white : otherCols[i];
			vh.AddUIVertexQuad(makeQuad(new Vector2(-size.x - offset, height - highlightWidth), new Vector2(size.x + offset, height), col));
		}
	}

	UIVertex[] makeQuad(Vector2 bottomLeft, Vector2 topRight, Color col) {
		UIVertex[] output = new UIVertex[4];
		UIVertex vert = UIVertex.simpleVert;
		vert.position = bottomLeft;
		vert.color = col;
		output[0] = vert;
		vert = UIVertex.simpleVert;
		vert.position = new Vector2(bottomLeft.x, topRight.y);
		vert.color = col;
		output[1] = vert;
		vert = UIVertex.simpleVert;
		vert.position = topRight;
		vert.color = col;
		output[2] = vert;
		vert = UIVertex.simpleVert;
		vert.position = new Vector2(topRight.x, bottomLeft.y);
		vert.color = col;
		output[3] = vert;
		return output;
	}
}
}
