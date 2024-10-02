interface ClosedSidenavProps {
    toggleExpanded(): void
}

export default function ClosedSidenav(props: ClosedSidenavProps)
{
    return (<div className="lg:hidden block w-auto h-full shadow-lg bg-green-300 text-center space-y-4 py-10 px-4">
                <button className="bg-green-600 rounded-lg py-4 px-2" onClick={() => props.toggleExpanded()}>Expand</button>
            </div>);
}