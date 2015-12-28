# TTUI-Framework
##TTUI Framework是什么？
**Ting Ting UI Framework**是一个供Unity3D引擎使用的简易的UI消息分发架构。基于NGUI。它的目录如下：
	
	//放置在Unity项目的Assets文件夹下：
	.
	├── .gitignore                    
	├── README.md
	├── NGUI					//使用的NGUI
	├── Resources				//存放测试中使用的UI页面
	│   └── TTUITest.prefab
	├── TTUI				//TTUI的文件夹
	│	├── Src                        // sources 
	│	│   ├── Constant              // 常量&辅助类  
	│	│   │   ├── TTUIFrameID.cs   //UI的ID在这里定义
	│	│   │   └── TTUIHelper.cs   //辅助类
	│ |   ├── Factory
	│	|   |   ├── TTUIFactory.cs	//抽象类，用于批量注册UI信息
	│	│   |   └── TTUIBuilder.cs	//注册UI信息的具体执行者
	│ |   ├── Manager				//管理器模块
	│	|   |   ├── TTUIFrameManager.cs	//UI页面管理模块
	│	│   |   └── TTUIMsgManager.cs	//UI消息管理模块
	│ |   ├── Message				//定义UI的Message
	│	│   |   └── TTUIMessage.cs	//所有UI消息的基类，自定义的UI消息需要继承该类
	│ |   ├── UI					//定义UI的页面
	│	│   |   └── TTUIFrame.cs	//所有UI类型的基类，定义了最基本的一些功能
	│ |   └── TTUICore.cs			//提供了对UI各个控制模块的引用，操作入口
	│ └──Test                  //测试脚本
	│     ├── Frame       //UI页面类，继承自TTUIFrame
	|     |   └── TTUITestFrame.cs
	│     ├── Item         //MonoBehaviour脚本，用来接受用户的点击
	|     |   ├── TTUITestItem.cs	//创建消息，并发送到消息管理器
	|     |   └── TTUITestItem1.cs
	│     ├── MonoScript         //捆绑在测试UI资源上的MonoBehav脚本
	|     |   └── TTUITestScript.cs 
	│     ├── Msg         //测试UI消息，继承自TTUIMessage
	|     |   └──  TTUITestMsg.cs    
	│     └── TTUITestFactory.cs	//继承自TTUIFactory，用来注册测试的UI信息
	|	  |
	|     └── TTUITestMain.cs    //测试入口 
	|   
	│       
	└── TTUITestScene.unity				//测试场景


##TTUI Framework如何测试？
###1.基本概念
####1.1 TTUIFrame
由于主要的思想是将UI资源注册、将逻辑和具体UI分离，因此我在**TTUIFrameID.cs**文件中首先定义了一些UI页面的ID：

	//TTUIFrameID.cs
	public class TTUIFrameID {
	    public const int InitTestFrame = 0x01;
	    public const int InitTestFrame2 = 0x02;
	    //TODO
	}
	
并且对每个UI页面进行了一层抽象，创建了一个不依赖于MonoBehaviour的UIFrame类——**TTUIFrame**。所有的具体UI页面的类都必须继承它。
例如在测试中，我定义的**TTUITestFrame**便继承于它。
这样，我们就可以通过注册UI的ID以及UI对应的UIFrame类来管理我们的UI系统了。而负责这个业务的类便是**TTUIFrameManager**类。

####1.2 TTUIMessage
除了将UIFrame注册管理之外，我还需要将消息和它对应的回调方法进行注册管理。因此也需要提供一个ID以及一个委托对象（作为回调函数）。
其中ID的部分使用了我在辅助类**TTUIHelper**中提供的哈希函数，将字符串转换成为ID进行注册。
	
	//TTUIMessage.cs
	public abstract class TTUIMessage  {
	    
	    public TTUIMessage(string name)
	    {
			this.ID = TTUIHelper.StringHash(name);
	    }
	    ...
    
同样，我还会提供一个类**TTUIMsgManager**用来管理UI消息。

###2.注册分发
####2.1注册UIFrame类
由于UI资源实例化的工作是通过抽象出来的UIFrame类来处理的，因此在注册UIFrame类时即需要关联UI的ID和其对应的UIFrame类，同时也要提供相应的资源路径信息。这里我使用了一个抽象工厂**TTUIFactory**来实现批量注册多个UI，在具体的环境中，用户需要继承该工厂，自己添加自定义的UI信息。例如Test中的**TTUITestFactory**，只添加了一个UI：
	
	public class TTUITestFactory : TTUIFactory {
	
		#region Public Methods
	
	    /// <summary>
		/// 以FrameID作为key，创建FrameID和对应的UI类之间的联系。
	    /// </summary>
		public override void BuildCommonUIFrame()
	    {
			BuildUIFrame<TTUITestFrame>(TTUIFrameID.InitTestFrame);
			//TODO
	    }
	
		/// <summary>
		///以FrameID作为key，注册UI资源的地址，用于创建id和prefab之间的关联
		///使用时，在次方法下添加具体的k-v(id-资源路径）。
		/// </summary>
		public override void RegisterUIFrameAssetes()
		{
			RegisterAssets(TTUIFrameID.InitTestFrame, "TTUITest");	
			//TODO
		}
	
	  	#endregion
	
	}


###3.使用Test
_需要注意的是，我并没有上传meta文件，因此可能会出现脚本引用丢失的问题_。但是我想这应该不是什么大问题。
需要注意的是，将测试入口**TTUITestMain.cs**脚本挂载好。在**TTUITestMain.cs**文件中：
首先创建TTUI中各个模块的引用：

	TTUICore.Create();
之后，在利用自己定义的TTUITestFactory工厂来注册页面信息：

	TTUITestFactory uifact = new TTUITestFactory();
	uifact.BuildCommonUIFrame();
	uifact.RegisterUIFrameAssetes();
同时，不要忘了要在Update函数中更新UI的各个模块：

		TTUICore.Update();	


