// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FX/Hologram"
{
	Properties
	{
		_BaseColour("Base Colour", Color) = (0,1,0.04915357,0)
		_Texture("Texture", 2D) = "white" {}
		_PanSpeed("Pan Speed", Vector) = (0,0,0,0)
		_Intensity("Intensity", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float4 screenPos;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _Texture;
		uniform float2 _PanSpeed;
		uniform float4 _BaseColour;
		uniform sampler2D _GrabTexture;
		uniform float _Intensity;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float2 panner71 = ( 1.0 * _Time.y * _PanSpeed + ase_grabScreenPosNorm.xy);
			float4 TextureCoordinates59 = ( 1.0 - tex2D( _Texture, panner71 ) );
			float4 screenColor4 = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD( ase_grabScreenPos ) );
			float4 Texture15 = ( ( TextureCoordinates59 * _BaseColour ) + screenColor4 );
			float4 color28 = IsGammaSpace() ? float4(1.498039,0.8392157,0.1647059,0) : float4(2.433049,0.6724432,0.02315337,0);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV27 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode27 = ( 0.0 + 0.5 * pow( 1.0 - fresnelNdotV27, 5.0 ) );
			float4 Fresnel30 = ( color28 * fresnelNode27 );
			float4 Everything66 = ( ( Texture15 * _Intensity ) + Fresnel30 );
			o.Emission = Everything66.rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred  

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
1866;4;1906;1044;233.1445;773.3133;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;69;321.4637,-734.5962;Float;False;1128.202;403.2262;Comment;6;21;19;12;59;71;72;;1,1,1,1;0;0
Node;AmplifyShaderEditor.GrabScreenPosition;21;371.4637,-661.6578;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;19;443.1416,-492.3699;Float;False;Property;_PanSpeed;Pan Speed;2;0;Create;True;0;0;False;0;0,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;71;645.656,-634.213;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;12;829.5076,-684.5962;Float;True;Property;_Texture;Texture;1;0;Create;True;0;0;False;0;None;b97db8acddac10d4c867939fcd38e487;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;72;1178.655,-652.4129;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;63;389.118,-276.7756;Float;False;1063.31;362.5261;Comment;6;1;4;13;5;15;62;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;59;1379.568,-689.1713;Float;False;TextureCoordinates;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;1;483.2378,-132.7848;Float;False;Property;_BaseColour;Base Colour;0;0;Create;True;0;0;False;0;0,1,0.04915357,0;0.01138768,0.2830189,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;62;439.118,-226.7756;Float;False;59;TextureCoordinates;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;863.77,-225.3053;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;55;569.6362,130.5278;Float;False;876.6881;514.8958;Comment;4;29;30;27;28;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenColorNode;4;830.5854,-121.2495;Float;False;Global;_GrabScreen0;Grab Screen 0;3;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;28;684.0773,182.3278;Float;False;Constant;_Color0;Color 0;4;1;[HDR];Create;True;0;0;False;0;1.498039,0.8392157,0.1647059,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;5;1040.578,-208.5105;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;27;619.6362,392.4232;Float;True;Standard;WorldNormal;ViewDir;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;942.6771,186.6924;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;68;226.4876,679.5554;Float;False;1220.104;383.1998;Comment;6;14;36;35;37;54;66;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;15;1209.427,-215.1456;Float;False;Texture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;14;368.2426,729.5554;Float;False;15;Texture;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;37;276.4876,805.6171;Float;False;Property;_Intensity;Intensity;3;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;1203.324,183.8687;Float;False;Fresnel;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;722.1943,737.5118;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;54;750.1083,947.7552;Float;False;30;Fresnel;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;993.5387,742.3099;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;66;1203.592,735.3295;Float;False;Everything;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;67;1655.632,49.66422;Float;False;66;Everything;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1887.578,9.73114;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;FX/Hologram;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Translucent;0.5;True;True;0;False;Opaque;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;1;;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;71;0;21;0
WireConnection;71;2;19;0
WireConnection;12;1;71;0
WireConnection;72;0;12;0
WireConnection;59;0;72;0
WireConnection;13;0;62;0
WireConnection;13;1;1;0
WireConnection;5;0;13;0
WireConnection;5;1;4;0
WireConnection;29;0;28;0
WireConnection;29;1;27;0
WireConnection;15;0;5;0
WireConnection;30;0;29;0
WireConnection;36;0;14;0
WireConnection;36;1;37;0
WireConnection;35;0;36;0
WireConnection;35;1;54;0
WireConnection;66;0;35;0
WireConnection;0;2;67;0
ASEEND*/
//CHKSM=D4580B96825387CF89DE8A68A718A08ADDE5114E