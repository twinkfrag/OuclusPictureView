using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class picChange : MonoBehaviour
{

	List<Texture2D> pics;
	GameObject target;
	string dir = @"./img/";

	// Use this for initialization
	void Start()
	{
		target = GameObject.Find("imgPlane");
		pics = new List<Texture2D>();

		var source = new DirectoryInfo(dir);
		if (!source.Exists) source.Create();
		foreach (var file in source
			.GetFiles().Where(x => 
				string.Compare(x.Extension, ".jpg", true) == 0 || 
				string.Compare(x.Extension, ".png", true) == 0))
		{
			var tex = new Texture2D(0, 0);
			tex.LoadImage(LoadBin(file.FullName));
			pics.Add(tex);
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (pics != null)
		{
			var pic = pics[(int)(Time.time / 10) % pics.Count];
			var height = pic.height;
			target.GetComponent<Renderer>().material.mainTexture = pic;
			//target.renderer.material.shader = Shader.Find("Transparent/Diffuse");
			target.transform.localScale = new Vector3((float)pic.width / pic.height, 1.0f, 1.0f);
		}
	}

	byte[] LoadBin(string path)
	{
		byte[] buf;
		using (FileStream fs = new FileStream(path, FileMode.Open))
		using (BinaryReader br = new BinaryReader(fs))
		{
			buf = br.ReadBytes((int)br.BaseStream.Length);
		}
		return buf;
	}
}
