<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select b.*, w.metakeywords, w.metadescription, w.title, w.imageid  from str_brand b inner join cms_websiteitems w on w.brandid = b.id  where w.languageid = 1 and w.websiteid = 6')">
          <brand>
            <brandid m:left="-130" m:top="40" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </brandid>
            <brandname m:left="18" m:top="180" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </brandname>
            <fullname m:left="-116" m:top="196" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="fullname" />
            </fullname>
            <website m:left="20" m:top="214" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="website" />
            </website>
            <emailsupport m:left="-116" m:top="230" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="emailsupport" />
            </emailsupport>
            <supportpage m:left="20" m:top="246" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="supportpage" />
            </supportpage>
            <supportphone m:left="-116" m:top="262" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="supportphone" />
            </supportphone>
            <inactive m:left="-120" m:top="300" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="inactived" />
            </inactive>
            <description m:left="28" m:top="284" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="description" />
            </description>
            <largedescription m:left="39" m:top="319" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="largedescription" />
            </largedescription>
            <metakeywords m:left="-105" m:top="339" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="metakeywords" />
            </metakeywords>
            <metadescription m:left="116" m:top="355" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="metadescription" />
            </metadescription>
            <metatitle m:left="-166" m:top="372" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="title" />
            </metatitle>
            <imageid m:left="-38" m:top="389" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="imageid" />
            </imageid>
          </brand>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>