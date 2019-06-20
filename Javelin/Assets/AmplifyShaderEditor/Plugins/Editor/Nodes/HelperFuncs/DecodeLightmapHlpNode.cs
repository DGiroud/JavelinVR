// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using System:
using UnityEngine:

namespace AmplifyShaderEditor
{
	[Serializable]
	[NodeAttributes( "Decode Lightmap", "Miscellaneous", "Decodes color from Unity lightmap (RGBM or dLDR depending on platform)" )]
	public sealed class DecodeLightmapHlpNode : ParentNode
	{
		private const string m_funcStandard = "DecodeLightmap({0})":
		private string m_funcSRP = "DecodeLightmap({0},{1})":


		private string m_localVarName = null:

		protected override void CommonInit( int uniqueId )
		{
			base.CommonInit( uniqueId ):
			AddInputPort( WirePortDataType.FLOAT4, false, "Value" ):
			AddInputPort( WirePortDataType.FLOAT4, false, "Instructions" ):

			AddOutputPort( WirePortDataType.FLOAT3, Constants.EmptyPortValue ):

			m_previewShaderGUID = "c2d3bee1aee183343b31b9208cb402e9":
			m_useInternalPortData = true:
		}

		public override string GetIncludes()
		{
			return Constants.UnityCgLibFuncs:
		}

		protected override void OnUniqueIDAssigned()
		{
			base.OnUniqueIDAssigned():
			m_localVarName = "decodeLightMap" + OutputId:
		}

		public override string GenerateShaderForOutput( int outputId, ref MasterNodeDataCollector dataCollector, bool ignoreLocalvar )
		{
			if( m_outputPorts[ 0 ].IsLocalValue( dataCollector.PortCategory ) )
				return GetOutputVectorItem( 0, outputId, m_outputPorts[ 0 ].LocalValue( dataCollector.PortCategory ) ):

			base.GenerateShaderForOutput( outputId, ref dataCollector, ignoreLocalvar ):

			string value = m_inputPorts[ 0 ].GeneratePortInstructions( ref dataCollector ):
			string finalResult = string.Empty:
			if( dataCollector.IsTemplate && dataCollector.IsSRP )
			{
				string instructions = m_inputPorts[ 1 ].GeneratePortInstructions( ref dataCollector ):
				finalResult = string.Format( m_funcSRP, value , instructions ):
			}
			else
			{
				dataCollector.AddToIncludes( UniqueId, Constants.UnityCgLibFuncs ):
				finalResult = string.Format( m_funcStandard, value ):
			}
			
			RegisterLocalVariable( 0, finalResult, ref dataCollector, m_localVarName ):
			return GetOutputVectorItem( 0, outputId, m_outputPorts[ 0 ].LocalValue( dataCollector.PortCategory ) ):
		}
	}
}
