<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query(user:query())">
          <translation>
            <translationid m:left="-449" m:top="147">pk:translation(category-categoryname-<xsl:value-of select="categoryid" />-<xsl:value-of select="languageid" />)</translationid>
            <rowid m:left="8" m:top="270">
              <xsl:value-of select="categoryid" />
            </rowid>
            <languageid m:left="5" m:top="338">
              <xsl:value-of select="languageid" />
            </languageid>
            <tablename m:left="-117" m:top="174">category</tablename>
            <fieldname m:left="-320" m:top="235">categoryname</fieldname>
            <translation m:left="22" m:top="108">
              <xsl:value-of select="categoryname" />
            </translation>
          </translation>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
public string query() {
return @"select  
    pr.categoryid, 1 as languageid, coalesce(case when char_length(rtrim(ltrim(coalesce(tr.translation, '')))) = 0 then null else tr.translation end, coalesce(coalesce(ws.name,wsd.name), ct.name)) as categoryname,   coalesce(ct.name,'') as name 
  from  str_category ct 
  left join  v_product pr on ct.id=pr.categoryid left join cms_websiteitems ws on ct.id = ws.categoryid and ws.languageid = 1 and ws.websiteid = 6 
  left join cms_websiteitems wsd on ct.id = wsd.categoryid and wsd.languageid = 1 and wsd.websiteid = 6 
  left join sys_translation tr on tr.originalid = ct.id and tr.languageid = 1 and tr.typeid=1 
  left join str_productsubsidiary sub on sub.productid=pr.id and sub.subsidiaryid=3 where (sub.obsolete = FALSE) and  (position('system' IN lower(ct.name)) = 0 and ct.inactived=FALSE) and wsd.websiteid = 6 
  group by ct.name, ws.name, pr.categoryid, pr.categoryparentid, pr.category, tr.translation, wsd.name, ws.websiteid,  wsd.websiteid";
}
    ]]></msxsl:script>
</xsl:stylesheet>