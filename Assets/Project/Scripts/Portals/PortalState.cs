
using UnityEngine;

public abstract class PortalState
{
    protected Portal portal;

    public PortalState(Portal portal)
    {
        this.portal = portal;
        setText(portal.portalText);
    }

    protected abstract void setText(SuperTextMesh textBox);
    public abstract void EnterPortal(BaseCharacter character);
}

public class PortalLocked : PortalState
{
    public PortalLocked(Portal portal) : base(portal)
    {
        this.portal = portal;
        setText(portal.portalText);
    }

    public override void EnterPortal(BaseCharacter character)
    {
        if (character.possessingSpirit.Souls - portal.costToEnter >= 0)
        {
            character.possessingSpirit.Souls -= portal.costToEnter;
            portal.connectedPortal.GetComponentInParent<TreasureRoom>().GenerateSpiritEssence(portal.costToEnter);
            portal.state = new PortalUnOpened(portal);
        }
    }

    protected override void setText(SuperTextMesh textBox)
    {
        textBox.text = "<w=seasick>" + portal.costToEnter.ToString() + " souls";
    }
}

public class PortalUnOpened : PortalState
{
    public PortalUnOpened(Portal portal) : base(portal)
    {
        this.portal = portal;
        setText(portal.portalText);
    }

    public override void EnterPortal(BaseCharacter character)
    {
        if (portal.connectedPortal.transform.parent.TryGetComponent(out WalkerGenerator nextRoom))
        {
            portal.StartCoroutine(nextRoom.SetRoomActive(character, portal.connectedPortal, portal.GetComponentInParent<Room>()));
        }
        else
        {
            portal.StartCoroutine(portal.Teleport(character));
        }
        portal.state = new PortalOpened(portal);
    }

    protected override void setText(SuperTextMesh textBox)
    {
        textBox.text = "<w=seasick>Not Entered";
    }
}

public class PortalOpened : PortalState
{
    public PortalOpened(Portal portal) : base(portal)
    {
        this.portal = portal;
        setText(portal.portalText);
    }

    public override void EnterPortal(BaseCharacter character)
    {
        if (portal.connectedPortal.transform.parent.TryGetComponent(out WalkerGenerator nextRoom))
        {
            portal.StartCoroutine(nextRoom.SetRoomActive(character, portal.connectedPortal, portal.GetComponentInParent<Room>()));
        }
        else
        {
            portal.StartCoroutine(portal.Teleport(character));
        }
    }

    protected override void setText(SuperTextMesh textBox)
    {
        textBox.text = "";
    }
}