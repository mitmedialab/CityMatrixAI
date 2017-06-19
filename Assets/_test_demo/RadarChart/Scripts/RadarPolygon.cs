namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Radar Chart")]
	public class RadarPolygon : MaskableGraphic
    {
        [SerializeField]
        Texture tex;
        public bool isFill = true;
        [Range(0, 0.99f)]
        public float fillPercent = 0.8f;
        [Range(3, 360)]
        public int segment = 3;
        [Range(0, 1f)]
        public float[] value = new float[3];
        [Range(0, 1f)]
        public float defaultValue = 1;
        [Range(0f, 360f)]
        public float angleOffset = 0;
        public bool useStateLine = true;
        public Color lineColor = Color.white;
        public float lineWidth = 0.5f;
        public bool useDefaultLength = true;
        public float lineLength = 50;

        public override Texture mainTexture
        {
            get
            {
                return tex == null ? s_WhiteTexture : tex;
            }
        }

        public void Update() {
          UpdateGeometry();
        }

        protected UIVertex[] GetVert(Vector2[] vertices, Vector2[] uvs, bool customColor = false)
        {
            UIVertex[] vbo = new UIVertex[4];
            for (int i = 0; i < vertices.Length; i++)
            {
                var vert = UIVertex.simpleVert;
                if (!customColor)
                    vert.color = color;
                else
                    vert.color = lineColor;
                vert.position = vertices[i];
                vert.uv0 = uvs[i];
                vbo[i] = vert;
            }
            return vbo;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            Vector2 size = this.GetComponent<RectTransform>().rect.size / 2;
            vh.Clear();
            float angle, s, c;
            Vector2[] pos = new Vector2[segment + 1];
            Vector2[] pos2 = new Vector2[segment];
            Vector2[] pos3 = new Vector2[segment];
            Vector2[] pos4 = new Vector2[segment];
            Vector2[] pos5 = new Vector2[segment];
            Vector2[] uv = new Vector2[4];
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 0);
            uv[3] = new Vector2(1, 1);
            for (int i = 0; i < segment; i++)
            {
                angle = angleOffset + (360 / (float)segment) * i;
                c = Mathf.Cos(angle * Mathf.Deg2Rad);
                s = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 point = new Vector2(c * size.x, s * size.y);
                if (i >= value.Length) pos[i] = point * defaultValue;
                else pos[i] = point * value[i];
                if (!isFill)
                {
                    if (i >= value.Length)
                    {
                        if ((1 - fillPercent) < defaultValue) pos2[i] = pos[i] - point * (1 - fillPercent);
                        else pos2[i] = Vector2.zero;
                    }
                    else
                    {
                        if ((1 - fillPercent) < value[i]) pos2[i] = pos[i] - point * (1 - fillPercent);
                        else pos2[i] = Vector2.zero;
                    }
                }
                if (useStateLine)
                {
                    pos3[i] = new Vector2(Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * lineWidth, Mathf.Sin((angle + 90) * Mathf.Deg2Rad) * lineWidth);
                    pos4[i] = new Vector2(Mathf.Cos((angle - 90) * Mathf.Deg2Rad) * lineWidth, Mathf.Sin((angle - 90) * Mathf.Deg2Rad) * lineWidth);
                    if (!useDefaultLength) pos5[i] = new Vector2(c * lineLength, s * lineLength);
                    else pos5[i] = point;
                }
            }
            pos[segment] = Vector2.zero;
            for (int i = 0; i < segment; i++)
            {
                if (i + 1 != segment)
                {
                    if (isFill) vh.AddUIVertexQuad(GetVert(new[] { pos[i], pos[i + 1], pos[segment], pos[segment] }, uv));
                    else
                    {
                        vh.AddUIVertexQuad(GetVert(new[] { pos[i], pos[i + 1], pos2[i + 1], pos2[i] }, uv));
                    }
                }
                else
                {
                    if (isFill) vh.AddUIVertexQuad(GetVert(new[] { pos[i], pos[0], pos[segment], pos[segment] }, uv));
                    else
                    {
                        vh.AddUIVertexQuad(GetVert(new[] { pos[i], pos[0], pos2[0], pos2[i] }, uv));
                    }
                }
            }
            if (useStateLine)
            {
                for (int i = 0; i < segment; i++)
                {
                    vh.AddUIVertexQuad(GetVert(new[] { pos5[i] + pos3[i], pos5[i] + pos4[i], pos[segment] + pos4[i], pos[segment] + pos3[i] }, uv, true));
                }
            }
        }
    }
}
