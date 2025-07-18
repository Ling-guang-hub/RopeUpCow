// Project  RopeUp
// FileName  YellowCattle.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 14:06:04 
//


public class YellowCattle: BaseCattle
{

    
    public void Start()
    {
        
    }

    private  void Awake()
    {
    }


    public override void InitData()
    {
        LastPos = transform.localPosition;
        createPos = transform.localPosition;
        StartDoMove();
    }
    
    
    
}
